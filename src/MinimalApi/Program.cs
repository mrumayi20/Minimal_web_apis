using MinimalApi.Repositories;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Now my application knows that we are using controllers.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

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


