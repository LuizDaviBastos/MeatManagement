using MeatManager.Data.Data;
using MeatManager.Data.Mapping;
using MeatManager.Data.Repositories;
using MeatManager.Model.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeatManager.Data
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
        {
            MapsterConfig.RegisterMappings();

            string? connectionString = configuration.GetConnectionString("DbConnection");
            services.AddScoped<MeatManagerContext>(x => new(connectionString));

            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IMeatRepository, MeatRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
        }
    }
}
