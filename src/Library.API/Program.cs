using Library.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();

await app.RunAsync();
