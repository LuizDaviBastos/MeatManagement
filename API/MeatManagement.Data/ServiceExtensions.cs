using MeatManager.Data.Data;
using MeatManager.Data.Entities;
using MeatManager.Data.Repositories;
using MeatManager.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeatManager.Data
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DbConnection");
            services.AddScoped<MeatManagerContext>(x => new(connectionString));

            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IMeatRepository, MeatRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
        }

        public static void SeedStatesAndCities(this ModelBuilder modelBuilder)
        {
            var state1 = new State { Id = Guid.Parse("1E3F5A1B-2D4C-4F8A-9E2A-1234567890AB"), Name = "São Paulo", UF = "SP" };
            var state2 = new State { Id = Guid.Parse("2B4C6D7E-3F8A-4B2C-9F3D-2345678901BC"), Name = "Rio de Janeiro", UF = "RJ" };
            var state3 = new State { Id = Guid.Parse("3C5D7E8F-4A1B-5C3D-0A4E-3456789012CD"), Name = "Minas Gerais", UF = "MG" };
            var state4 = new State { Id = Guid.Parse("4D6E7F8A-1B2C-3D4E-5F6A-4567890123EE"), Name = "Paraná", UF = "PR" };
            var state5 = new State { Id = Guid.Parse("5E7F8A9B-2C3D-4E5F-6A7B-5678901234FF"), Name = "Rio Grande do Sul", UF = "RS" };
            var state6 = new State { Id = Guid.Parse("6F8A9B0C-3D4E-5F6A-7B8C-6789012345AA"), Name = "Bahia", UF = "BA" };
            var state7 = new State { Id = Guid.Parse("7A9B0C1D-4E5F-6A7B-8C9D-7890123456BB"), Name = "Distrito Federal", UF = "DF" };

            modelBuilder.Entity<State>().HasData(state1, state2, state3, state4, state5, state6, state7);

            modelBuilder.Entity<City>().HasData(
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "São Paulo", StateId = state1.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "Campinas", StateId = state1.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "Santos", StateId = state1.Id },

                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Name = "Rio de Janeiro", StateId = state2.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000005"), Name = "Niterói", StateId = state2.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000006"), Name = "Petrópolis", StateId = state2.Id },

                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000007"), Name = "Belo Horizonte", StateId = state3.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000008"), Name = "Uberlândia", StateId = state3.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000009"), Name = "Ouro Preto", StateId = state3.Id },

                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000010"), Name = "Curitiba", StateId = state4.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000011"), Name = "Londrina", StateId = state4.Id },

                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000012"), Name = "Porto Alegre", StateId = state5.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000013"), Name = "Caxias do Sul", StateId = state5.Id },

                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000014"), Name = "Salvador", StateId = state6.Id },
                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000015"), Name = "Feira de Santana", StateId = state6.Id },

                new City { Id = Guid.Parse("10000000-0000-0000-0000-000000000016"), Name = "Brasília", StateId = state7.Id }
            );
        }
    }
}
