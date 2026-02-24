namespace MinimalApi.Models.DTOs
{
    public class ProductUpdateDto
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}