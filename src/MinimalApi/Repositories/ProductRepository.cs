using MinimalApi.Data;
using MinimalApi.Models.Entities;

namespace MinimalApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        //_context (The DB Coordinator)
        // Products (The DbSet)
        public void AddProduct(Product product)
        {
            _context.Products.Add(product); // SQL Server handles the ID auto-increment!
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void UpdateProduct(int id, Product product)
        {
            var oldProduct = _context.Products.Find(id);
            if (oldProduct != null)
            {
                _context.Products.Remove(oldProduct);
                _context.Products.Add(product);
                _context.SaveChanges();
            }

        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}