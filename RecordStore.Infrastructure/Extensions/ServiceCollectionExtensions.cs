﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordStore.Infrastructure.Persistence;

namespace RecordStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("RecordStore"));
                using (var serviceProvider = services.BuildServiceProvider())
                {
                    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.EnsureCreated();
                    dbContext.Seed();
                    dbContext.SaveChanges();
                }
                return services;
            }

            string connectionString = environment.IsProduction()
                ? configuration.GetConnectionString("ProductionConnection") ?? throw new ApplicationException($"No 'ProductionConnection' connection string found in configuration. Environment = '{environment.EnvironmentName}'")
                : configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException($"No 'DefaultConnection' connection string found in configuration. Environment = '{environment.EnvironmentName}'");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
