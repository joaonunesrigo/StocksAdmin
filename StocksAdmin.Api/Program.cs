using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StocksAdmin.Api;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DependencyInjections.AddApplicationServices(builder.Services, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ExternalApiConfig.SecretJWT)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPoliticaCors", policy =>
    {
        policy.AllowAnyOrigin()    // Permite qualquer origem (dom�nio)
              .AllowAnyHeader()   // Permite qualquer cabe�alho
              .AllowAnyMethod();  // Permite qualquer m�todo (GET, POST, PUT, DELETE, etc.)
    });
});

// Habilita o middleware CORS usando a pol�tica configurada

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MinhaPoliticaCors");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
