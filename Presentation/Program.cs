using Application.Interfaces;
using Application.UseCase;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom
builder.Services.AddSingleton<StoreDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductQuery, ProductQuery>();
builder.Services.AddScoped<IProductCommand, ProductCommand>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
