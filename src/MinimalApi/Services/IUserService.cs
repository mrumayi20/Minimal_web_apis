using MinimalApi.Models.DTOs;

namespace MinimalApi.Services
{
    public interface IUserService
    {
        public void AddUser(LoginDto user);

        public List<LoginDto> GetAllUsers();

        public string? GetUserPasswordByUsername(string username);

        public RegisterDto? GetUserByUsername(string username);
    }
}