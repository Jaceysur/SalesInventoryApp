using SalesInventoryApp.Models;
using SalesInventoryApp.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SalesInventoryApp
{
    public partial class NewSaleForm : Form
    {
        private readonly InMemoryRepository _repo;
        // Create product and sales repository interfaces for clear access
        private readonly IProductRepository _productRepo;
        private readonly ISalesRepository _salesRepo;

        public NewSaleForm(InMemoryRepository repo)
        {
            _repo = repo;
            // FIX: Initialize the interface references by casting the concrete repo
            _productRepo = (IProductRepository)repo;
            _salesRepo = (ISalesRepository)repo;

            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            // FIX: Use IProductRepository.GetAll()
            var products = _productRepo.GetAll().Where(p => p.Stock > 0).ToList();

            cboProduct.DataSource = products;
            cboProduct.DisplayMember = "Name";
            cboProduct.ValueMember = "Id";

            if (products.Any()) cboProduct.SelectedIndex = 0;

            OnProductChanged();
        }

        private void OnProductChanged(object sender = null, EventArgs e = null)
        {
            if (cboProduct.SelectedItem is Product p)
            {
                txtUnitPrice.Text = $"{p.Price:F2}";
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            if (!decimal.TryParse(txtUnitPrice.Text, out decimal unit)) unit = 0;

            var qty = (int)nudQty.Value;
            txtTotal.Text = (unit * qty).ToString("F2");
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {

            var qty = (int)nudQty.Value;

            if (p.Stock < qty)
            {
                MessageBox.Show("Insufficient stock for this sale!");
                return;
            }

            var sale = new Sale
            {
                Id = SalesInventoryApp.Data.DataStore.Instance.NextSaleId++,
                Date = DateTime.Today,
                ProductId = p.Id,
                ProductName = p.Name,
                Quantity = qty,
                UnitPrice = p.Price
            };

            // FIX: Use ISalesRepository.Add()
            _salesRepo.Add(sale);

            p.Stock -= qty;

            // FIX: Use IProductRepository.Update()
            _productRepo.Update(p);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}