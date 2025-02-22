using Library.DataAccess.DataContext;
using Library.DataAccess.Repositories;
using Library.DataAccess.Repositories.Interfaces;
using Library.DataAccess.Services;
using Library.DataAccess.Services.Interfaces;
using Library.Models.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore;
using System.Text;
using System.Text.Json.Serialization;
namespace Library.API.Extensions;

public static partial class HostConfiguration
{
    static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        var jwtSettings = new JwtSettings();

        builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        builder
            .Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
        builder.Services.AddAuthorization();

        return builder;
    }

    static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddRouting(o => o.LowercaseUrls = true)
            .AddControllers()
            .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;

        return builder;
    }

    static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<AppDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

        return builder;
    }

    static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return builder;
    }

    static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IBookServie, BookService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITokenGeneratorService, TokenGeneratorService>()
            .AddScoped<IAuthService, AuthService>();



        return builder;
    }

    static WebApplication UseAuth(this WebApplication app)
    {
        app
            .UseAuthentication()
            .UseAuthorization();

        return app;
    }

    static WebApplication UseExposers(this WebApplication app)
    {
        app
            .MapControllers();

        return app;
    }
    
    static WebApplication UseDevTools(this WebApplication app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        return app;
    }
}
