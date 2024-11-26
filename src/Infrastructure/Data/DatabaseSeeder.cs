using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Data
{
    public class DatabaseSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SeedData(_serviceProvider);
        }

        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}