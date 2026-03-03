using MinimalApi.Models.Entities;

namespace MinimalApi.Models.Repositories
{
    public interface IUserRepository
    {
        public void AddUser(User user);

        public List<User> GetAllUsers();

        public string? GetUserPasswordByUsername(string username);

        public User? GetUserByUsername(string username);
    }
}