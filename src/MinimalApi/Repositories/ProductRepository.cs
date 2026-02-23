using MinimalApi.Models.Entities;

namespace MinimalApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _context = new();

        public void AddProduct(Product product)
        {
            _context.Add(product);
        }

        public List<Product> GetAllProducts()
        {
            return _context;
        }

        public Product? GetProductById(int id)
        {
            if (id < 0 || id > _context.Count - 1)
            {
                return null;
            }

            var product = _context.Where(p => p.Id == id).FirstOrDefault();

            return product;
        }
    }
}