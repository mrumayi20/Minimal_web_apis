using MinimalApi.Data;
using MinimalApi.Models.Entities;

namespace MinimalApi.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public string? GetUserPasswordByUsername(string username)
        {
            var user_pass = _context.Users.Where(u => u.Username == username)
            .Select(u => u.PasswordHash)
            .FirstOrDefault();

            return user_pass;
        }

        public User? GetUserByUsername(string username)
        {
            var user = _context.Users.Where(u => u.Username == username)
            .FirstOrDefault();

            return user;
        }
    }
}