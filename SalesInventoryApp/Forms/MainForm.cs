using SalesInventoryApp.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D; // <--- ADDED for LinearGradientBrush
using Microsoft.VisualBasic; // <--- ADDED for Interaction.InputBox

namespace SalesInventoryApp
{
    public partial class MainForm : Form
    {
        // NOTE: If InMemoryRepository is the concrete type, you should ideally use the interfaces
        // private readonly IProductRepository _productRepo;
        // private readonly ISalesRepository _salesRepo;
        // However, for this fix, we will keep the single concrete type and cast it.
        private readonly InMemoryRepository _repo = new InMemoryRepository();

        public MainForm()
        {
            InitializeComponent();
            this.Paint += MainForm_Paint;
            RefreshAll();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // Fixed brush usage
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                System.Drawing.Color.FromArgb(102, 126, 234),
                System.Drawing.Color.FromArgb(118, 75, 162),
                45F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        // ... (other event handlers)

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            UpdateDashboard();
            UpdateInventory();
            UpdateSales();
        }

        private void UpdateDashboard()
        {
            // FIX: Cast to IProductRepository for Product methods
            var productRepo = (IProductRepository)_repo;
            // FIX: Cast to ISalesRepository for Sales methods
            var salesRepo = (ISalesRepository)_repo;

            // Use IProductRepository
            var prods = productRepo.GetAll().ToList();
            lblTotalProducts.Text = prods.Count.ToString();
            lblLowStock.Text = prods.Count(p => p.Stock < 10).ToString();

            var today = System.DateTime.Today;

            // Use ISalesRepository for sales data
            var allSales = salesRepo.GetAll().ToList();

            var todaySales = allSales.Where(s => s.Date.Date == today).Sum(s => s.Total);
            lblTodaySales.Text = $"${todaySales:F2}";
            lblTotalRevenue.Text = $"${allSales.Sum(s => s.Total):F2}";
        }

        private void UpdateInventory()
        {
            // FIX: Cast to IProductRepository
            dgvInventory.DataSource = null;
            dgvInventory.DataSource = ((IProductRepository)_repo).GetAll().ToList();
        }

        private void UpdateSales()
        {
            // FIX: Cast to ISalesRepository
            dgvSales.DataSource = null;
            dgvSales.DataSource = ((ISalesRepository)_repo).GetAll().ToList();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Pass the concrete repository instance
            var dlg = new AddProductForm(_repo);
            if (dlg.ShowDialog() == DialogResult.OK) RefreshAll();
        }

        private void btnNewSale_Click(object sender, EventArgs e)
        {
            // Pass the concrete repository instance
            var dlg = new NewSaleForm(_repo);
            if (dlg.ShowDialog() == DialogResult.OK) RefreshAll();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var rpt = new ReportsForm(_repo);
            rpt.ShowDialog();
            RefreshAll();
        }

        private void btnEditStock_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0) return;

            // FIX: Cast to IProductRepository for GetById, Update
            var productRepo = (IProductRepository)_repo;

            var id = (int)dgvInventory.SelectedRows[0].Cells["Id"].Value;
            var p = productRepo.GetById(id);

            // FIX: Use 'Interaction' now that the 'using' statement is present
            var input = Interaction.InputBox($"Update stock for {p.Name}:", "Update Stock", p.Stock.ToString());

            if (int.TryParse(input, out int newStock))
            {
                p.Stock = newStock;
                productRepo.Update(p);
                RefreshAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0) return;

            // FIX: Cast to IProductRepository for Delete
            var productRepo = (IProductRepository)_repo;

            var id = (int)dgvInventory.SelectedRows[0].Cells["Id"].Value;
            var confirm = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                productRepo.Delete(id);
                RefreshAll();
            }
        }
    }
}