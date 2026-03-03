using MinimalApi.Models.DTOs;
using MinimalApi.Models.Repositories;
using MinimalApi.Models.Entities;


namespace MinimalApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(LoginDto user)
        {
            _userRepository.AddUser(new User
            {
                Username = user.Username,
                PasswordHash = user.Password
            });
        }

        public List<LoginDto> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            return users.Select(u => new LoginDto
            {
                Username = u.Username,
                Password = u.PasswordHash
            }).ToList();

        }

        public string? GetUserPasswordByUsername(string username)
        {
            var user = _userRepository.GetUserPasswordByUsername(username);
            return user;
        }

        public RegisterDto? GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return null;

            return new RegisterDto
            {
                Username = user.Username,
                Password = user.PasswordHash
            };
        }
    }
}