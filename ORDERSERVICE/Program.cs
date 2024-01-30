using AuctionMessageBus;
using Microsoft.EntityFrameworkCore;
using ORDERSERVICE.Data;
using ORDERSERVICE.Extensions;
using ORDERSERVICE.Service;
using ORDERSERVICE.Service.Iservice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddAuth();
builder.AddSwaggenGenExtension();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

builder.Services.AddScoped<IBid, BidService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();

builder.Services.AddHttpClient("Bid", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:BidService")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMigrations();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
