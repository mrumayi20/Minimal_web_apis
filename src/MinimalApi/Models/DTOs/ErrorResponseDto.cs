namespace MinimalApi.Models.DTOs
{
    public record ErrorResponse(int StatusCode, string Message, string? Details = null);
}