using FluentValidation;
using MeatManager.Service.DTOs;
using MeatManager.Service.Interfaces;
using MeatManager.Service.Mapping;
using MeatManager.Service.Services;
using MeatManager.Service.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeatManager.Service
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<MeatDto>, MeatDtoValidator>();
            services.AddScoped<IValidator<BuyerDto>, BuyerDtoValidator>();
            services.AddScoped<IValidator<OrderDto>, OrderDtoValidator>();

            services.AddSingleton<ICurrencyConversionService, CurrencyConversionService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMeatService, MeatService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBuyerService, BuyerService>();

            services.AddAutoMapper(x =>
            {
                x.AddProfile<ModelToDtoProfile>();
                
            });
        }
    }
}
