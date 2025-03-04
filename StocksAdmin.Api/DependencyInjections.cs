using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ProductClientHub.Api.DataBase;
using ProductClientHub.Api.Services.ExternalApiService;
using ProductClientHub.Api.Services.Stocks;
using ProductClientHub.Api.Services.User;
using ProductClientHub.Api.Services.Wallet;
using ProductClientHub.Api.Tasks;

namespace ProductClientHub.Api
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DbContextSettings:ConnectionString").Value;
            services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<HttpClient>();
            services.AddHostedService<AtualizaPrecoAtivosTask>();
            services.AddTransient<JwtTokenService>();
            services.AddScoped<WalletService>();
            services.AddScoped<StockService>();
            services.AddScoped<UserService>();
            services.AddScoped<AssetService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            return services;
        }
    }
}
