using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Repositories;
using MinimalApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using MinimalApi.Models.Repositories;
using MinimalApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Now my application knows that we are using controllers.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//registering AppDbContext with the dependency injection container 
//and configuring it to use SQL Server with the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Checks if the generated JWT token is valid or not by validating the token's signature, issuer, audience, and expiration time against the specified parameters.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

//Register Global Exception handler Service
builder.Services.AddExceptionHandler<GlobalExceptionHandle>();
builder.Services.AddProblemDetails(); //// Required for modern error handling

var app = builder.Build();

//Add to the pipeline (must be very early in the pipeline)
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//Enabling authentication and authorization middleware in the request pipeline
//After HttpDirection and before mapping controllers to ensure that all incoming requests are authenticated and authorized before reaching the controllers.
app.UseAuthentication();
app.UseAuthorization();

//mapping controllers
app.MapControllers();

app.Run();


