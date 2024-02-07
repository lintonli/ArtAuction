using Auction_Gateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.AddAuth();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().GetAwaiter().GetResult();
app.Run();
