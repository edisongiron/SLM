using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServindAp.Application.Interfaces.Repositories;
using ServindAp.Infrastructure.Data;
using ServindAp.Infrastructure.Persistence;

namespace ServindAp.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ServindApDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IPrestamoRepository, PrestamoRepository>();
            services.AddScoped<IHerramientaRepository, HerramientaRepository>();
            services.AddScoped<IPrestamoHerramientaRepository, PrestamoHerramientaRepository>();

            return services;
        }

        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ServindApDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
