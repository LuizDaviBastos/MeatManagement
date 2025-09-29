using MeatManager.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace MeatManager.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void SetupDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var context = scope.ServiceProvider.GetRequiredService<MeatManagerContext>();

                if (env.IsDevelopment())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
