using Application.Interfaces;
using Application.Models;
using Application.UseCase;
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
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductQuery, ProductQuery>();
builder.Services.AddTransient<ProductDto>();



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
