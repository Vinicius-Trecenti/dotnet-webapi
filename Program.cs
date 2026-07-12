using PetShop.Api.Repositories;
using PetShop.Api.Services;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Configura o contexto do banco de dados para usar SQLite.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=petshop.db"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
