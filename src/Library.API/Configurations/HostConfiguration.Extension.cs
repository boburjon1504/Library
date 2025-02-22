using Library.API.Services;
using Library.API.Services.Interfaces;
using Library.DataAccess.DataContext;
using Library.DataAccess.Repositories;
using Library.DataAccess.Repositories.Interfaces;
using Library.Models.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using FluentValidation;
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
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());

        builder
            .Services
            .AddAutoMapper(assemblies)
            .AddValidatorsFromAssemblies(assemblies)
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }});
            });

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
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IRequestUserContext, RequestUserContext>();



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
