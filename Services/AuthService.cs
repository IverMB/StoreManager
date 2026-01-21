using StoreManager.Models;

namespace StoreManager.Services
{
    public class AuthService
    {
        private readonly UserService _userService;

        public User? CurrentUser { get; private set; }

        public AuthService(UserService userService)
        {
            _userService = userService;
            EnsureAdminExists();
        }

        private void EnsureAdminExists()
        {
            var users = _userService.GetInternalUsers();

            if (users.Any(u => u.Email == "admin"))
                return;

            users.Add(new User
            {
                    Email = "admin",
                    Name = "Admin",
                    Password = "admin", 
                    Role = Roles.Administrator
                });
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = _userService.GetInternalUsers()
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
                return false;

            CurrentUser = user;
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        // Helper properties til Razor pages
        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => CurrentUser?.Role == Roles.Administrator;
        public bool IsSupport => CurrentUser?.Role == Roles.Support;
    }
}
