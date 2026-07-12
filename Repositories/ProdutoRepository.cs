// Repositories/ProdutoRepository.cs
using PetShop.Api.Models;

namespace PetShop.Api.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly List<Produto> _produtos = new();
    private int _proximoId = 1;

    public IEnumerable<Produto> Listar() => _produtos;

    public Produto? ObterPorId(int id) =>
        _produtos.FirstOrDefault(p => p.Id == id);

    public Produto Adicionar(Produto produto)
    {
        produto.Id = _proximoId++;   // o repositório controla o Id
        _produtos.Add(produto);
        return produto;
    }

    public bool Atualizar(Produto produto)
    {
        var existente = ObterPorId(produto.Id);
        if (existente is null) return false;

        existente.Nome = produto.Nome;
        existente.Preco = produto.Preco;
        existente.Estoque = produto.Estoque;
        return true;   // false = não achou o Id
    }

    public bool Remover(int id)
    {
        var produto = ObterPorId(id);
        if (produto is null) return false;

        _produtos.Remove(produto);
        return true;
    }
}