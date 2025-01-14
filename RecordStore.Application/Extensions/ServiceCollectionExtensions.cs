using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordStore.Application.Persistence;

namespace RecordStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            string connectionString;

            if (environment.IsDevelopment()) 
                connectionString = configuration.GetConnectionString("DevelopmentConnection") ?? throw new ApplicationException("No 'DevelopmentConnection' connection string found in configuration.");

            if (environment.IsProduction()) 
                connectionString = configuration.GetConnectionString("ProductionConnection") ?? throw new ApplicationException("No 'ProductionConnection' connection string found in configuration.");

            else 
                connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException("No 'DefaultConnection' connection string found in configuration.");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
