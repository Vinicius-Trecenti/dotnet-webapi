// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PetShop.Api.Models;

namespace PetShop.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Cada DbSet vira uma tabela no banco.
    public DbSet<Produto> Produtos => Set<Produto>();
}   