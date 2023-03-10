using common;
using data.Repository.Interfaces;
using data.Repository;
using data;
using Microsoft.EntityFrameworkCore;
using service.Services.Interfaces;
using service.Services;
using Microsoft.AspNetCore.Diagnostics;

namespace api.Extensions
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
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryNameService, CountryNameService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IStateNameService, StateNameService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityNameService, CityNameService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ITimezoneService, TimezoneService>();
        }

        public static void MigrateDatabase(this IServiceCollection services)
        {
            var dbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                    //.AllowAnyOrigin()
                    .WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:8013",
                        "https://addressbook-web.efcorebeginner.com")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));
        }
    }
}
