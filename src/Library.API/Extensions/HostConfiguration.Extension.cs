using Library.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
namespace Library.API.Extensions;

public static partial class HostConfiguration
{
    public static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddRouting(o => o.LowercaseQueryStrings = true)
            .AddControllers();

        return builder;
    }

    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<AppDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

        return builder;
    }

    public static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return builder;
    }

    public static WebApplication UseExposers(this WebApplication app)
    {
        app
            .MapControllers();

        return app;
    }
    public static WebApplication UseDevTools(this WebApplication app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        return app;
    }
}
