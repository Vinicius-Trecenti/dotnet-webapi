using Microsoft.EntityFrameworkCore;
using PetShop.Api.Data;
using PetShop.Api.Models;

namespace PetShop.Api.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ListarAsync() =>
        await _context.Produtos.ToListAsync();

    public async Task<Produto?> ObterPorIdAsync(int id) =>
        await _context.Produtos.FindAsync(id);

    public async Task<Produto> AdicionarAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<bool> AtualizarAsync(Produto produto)
    {
        var existente = await _context.Produtos.FindAsync(produto.Id);
        if (existente is null) return false;

        existente.Nome = produto.Nome;
        existente.Preco = produto.Preco;
        existente.Estoque = produto.Estoque;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto is null) return false;

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }
}