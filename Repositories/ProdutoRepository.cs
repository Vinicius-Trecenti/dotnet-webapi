// Repositories/ProdutoRepository.cs
using PetShop.Api.Data;
using PetShop.Api.Models;

namespace PetShop.Api.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    // O DbContext chega pronto via injeção de dependência.
    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> Listar() => _context.Produtos.ToList();

    public Produto? ObterPorId(int id) =>
        _context.Produtos.Find(id);

    public Produto Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();   // aqui o dado vai pro banco de verdade
        return produto;           // o Id agora é gerado pelo banco
    }

    public bool Atualizar(Produto produto)
    {
        var existente = _context.Produtos.Find(produto.Id);
        if (existente is null) return false;

        existente.Nome = produto.Nome;
        existente.Preco = produto.Preco;
        existente.Estoque = produto.Estoque;
        _context.SaveChanges();
        return true;
    }

    public bool Remover(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto is null) return false;

        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return true;
    }
}