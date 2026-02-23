using MinimalApi.Models.Entities;

namespace MinimalApi.Repositories
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);

        public List<Product> GetAllProducts();

        public Product? GetProductById(int id);
    }
}