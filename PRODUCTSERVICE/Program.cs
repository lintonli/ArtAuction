using Microsoft.EntityFrameworkCore;
using PRODUCTSERVICE.Data;
using PRODUCTSERVICE.Extensions;
using PRODUCTSERVICE.Services;
using PRODUCTSERVICE.Services.IServices;
using PRODUCTSERVICE.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddAuth();
builder.AddSwaggenGenExtension();
builder.Services.AddScoped<IProducts, ProductService>();
builder.Services.AddScoped<ICategory, CartService>();   
builder.Services.AddScoped<IBid, BidService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient("Category", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CartService")));
builder.Services.AddHttpClient("Bid", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:BidService")));
builder.Services.AddHostedService<TimeBackgroundService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

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
