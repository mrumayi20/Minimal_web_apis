var builder = WebApplication.CreateBuilder(args);

//Now my application knows that we are using controllers.
builder.Services.AddControllers();

builder.Services.AddOpenApi();

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


