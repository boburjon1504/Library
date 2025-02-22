namespace Library.API.Extensions;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddDbContext()
            .AddServices()
            .AddAuth()
            .AddExposers()
            .AddDevTools();

        return new ValueTask<WebApplicationBuilder>(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseAuth()
            .UseExposers();

        return new ValueTask<WebApplication> (app);
    }
}
