using common;
using data;
using data.Repository;
using data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using service.Services;
using service.Services.Interfaces;

namespace ui.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
                SecretUtility.SqlServer));
        }

        public static void ConfigureEnvironmentVariables(this IServiceCollection services)
        {
            DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "saqib-laptop.env"));
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
        }

        public static void MigrateDatabase(this IServiceCollection services)
        {
            var dbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
