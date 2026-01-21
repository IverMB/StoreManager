using StoreManager.Models;

namespace StoreManager.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();

        public List<User> GetInternalUsers()
        {
            return _users;
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users);
        }

        public Task AddUserAsync(User user)
        {
            if (_users.Any(u => u.Email == user.Email))
                throw new InvalidOperationException("Email skal være unik");

            user.CreatedOn = DateTime.UtcNow;
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateUserAsync(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Email == user.Email);
            if (existing == null)
                throw new InvalidOperationException("Bruger findes ikke");

            if (!string.IsNullOrWhiteSpace(user.Name))
                existing.Name = user.Name;
            
            if (!string.IsNullOrWhiteSpace(user.Password))
                existing.Password = user.Password;

            existing.Role = user.Role;

            return Task.CompletedTask;
        }

        public Task DeleteUserAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new InvalidOperationException("Bruger findes ikke");

            if (user.Email == "admin")
                throw new InvalidOperationException("Admin-brugeren kan ikke slettes");

            _users.Remove(user);
            return Task.CompletedTask;
        }
    }
}
