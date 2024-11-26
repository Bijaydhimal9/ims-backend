using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Data;

namespace Infrastructure.Configurations
{
    public class DatabaseInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DatabaseInitializer(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            await SeedUsersAsync();
        }

        private async Task SeedUsersAsync()
        {
            if (!_userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, "Admin@143");
            }
        }
    }
}