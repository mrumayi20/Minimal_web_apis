using MinimalApi.Models.DTOs;
using MinimalApi.Models.Entities;
using MinimalApi.Repositories;

namespace MinimalApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void AddProduct(ProductDto productDto)
        {
            _productRepository.AddProduct(new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            });
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts().Select(p =>
                                    new ProductDto
                                    {
                                        Name = p.Name,
                                        Price = p.Price
                                    }).ToList();

            return products;
        }

        public ProductDto? GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
