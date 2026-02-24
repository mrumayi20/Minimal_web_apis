using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Repositories;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Now my application knows that we are using controllers.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//registering AppDbContext with the dependency injection container 
//and configuring it to use SQL Server with the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//mapping controllers
app.MapControllers();

app.Run();


