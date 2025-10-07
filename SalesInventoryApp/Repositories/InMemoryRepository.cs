using SalesInventoryApp.Data;
using SalesInventoryApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesInventoryApp.Repositories
{
    public class InMemoryRepository : IProductRepository, ISalesRepository
    {
        private readonly DataStore _store = DataStore.Instance;

        // =========================================================
        // IProductRepository Implementation (Explicit for GetAll)
        // =========================================================

        // Explicitly implements IProductRepository.GetAll()
        IEnumerable<Product> IProductRepository.GetAll() => _store.Products.ToList();

        // The rest of the Product methods can remain implicit if desired, 
        // as their signatures are unique within the class.
        public Product GetById(int id) => _store.Products.FirstOrDefault(p => p.Id == id);

        public void Add(Product p) => _store.Products.Add(p);

        public void Update(Product p)
        {
            var existing = GetById(p.Id);
            if (existing != null)
            {
                existing.Name = p.Name;
                existing.SKU = p.SKU;
                existing.Price = p.Price;
                existing.Stock = p.Stock;
            }
        }

        public void Delete(int id)
        {
            var p = GetById(id);
            if (p != null) _store.Products.Remove(p);
        }

        // =========================================================
        // ISalesRepository Implementation (Explicit for GetAll and Add)
        // =========================================================

        // Explicitly implements ISalesRepository.GetAll() to resolve the conflict
        IEnumerable<Sale> ISalesRepository.GetAll() => _store.Sales.ToList();

        // The Add(Sale s) method also conflicts with Add(Product p), 
        // so it must also be explicitly implemented or renamed in the interface/class.
        // Assuming we keep the interface as is, we use explicit implementation.
        void ISalesRepository.Add(Sale s) => _store.Sales.Add(s);
    }
}