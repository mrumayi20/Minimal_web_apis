using MinimalApi.Models.DTOs;

namespace MinimalApi.Services
{
    public interface IProductService
    {
        public void AddProduct(ProductDto productDto);

        public List<ProductDto> GetAllProducts();

        public ProductDto? GetProductById(int id);

        public void UpdateProduct(int id, ProductUpdateDto productUpdateDto);

        public void DeleteProduct(int id);
    }
}