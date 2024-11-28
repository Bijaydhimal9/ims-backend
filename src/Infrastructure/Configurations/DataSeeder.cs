using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Configurations;
public class DataSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private const string DefaultAdminEmail = "admin@gmail.com";
    private const string DefaultAdminPassword = "BookingSystem@123";
    private static readonly string[] DefaultRoles = { "Admin", "Manager", "User" };

    public DataSeeder(
        IServiceProvider serviceProvider
      )
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await SeedRolesAsync(roleManager);
        var adminUser = await SeedAdminUserAsync(userManager);
        await SeedChargeDataAsync(adminUser.Id);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in DefaultRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task<ApplicationUser> SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        var adminUser = await userManager.FindByEmailAsync(DefaultAdminEmail);
        if (adminUser != null) return adminUser;

        adminUser = new ApplicationUser
        {
            UserName = DefaultAdminEmail,
            Email = DefaultAdminEmail,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
        };

        var result = await userManager.CreateAsync(adminUser, DefaultAdminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        return adminUser;
    }

    private async Task SeedChargeDataAsync(string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var charges = GetDefaultCharges(userId);

        foreach (var charge in charges.Where(charge =>
            !dbContext.Charges.Any(c => c.ChargeCode == charge.ChargeCode)))
        {
            dbContext.Charges.Add(charge);
        }

        await dbContext.SaveChangesAsync();
    }

    private static IEnumerable<Charge> GetDefaultCharges(string userId) => new[]
    {
            new Charge
            {
                ChargeName = "First Degree Murder",
                ChargeCode = "C001",
                Description = "Intentional killing with premeditation",
                Status = ChargeStatus.Active,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            },
            new Charge
            {
                ChargeName = "Second Degree Murder",
                ChargeCode = "C002",
                Description = "Intentional killing without premeditation",
                Status = ChargeStatus.Active,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            }
        };

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}