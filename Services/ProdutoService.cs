// Services/ProdutoService.cs
using PetShop.Api.Models;
using PetShop.Api.Repositories;

namespace PetShop.Api.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;

    // O repositório chega pronto aqui — injeção de dependência.
    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Produto> Listar() => _repository.Listar();

    public Produto? ObterPorId(int id) => _repository.ObterPorId(id);

    public Produto Criar(Produto produto)
    {
        Validar(produto);
        return _repository.Adicionar(produto);
    }

    public bool Atualizar(Produto produto)
    {
        Validar(produto);
        return _repository.Atualizar(produto);
    }

    public bool Remover(int id) => _repository.Remover(id);

    // Regras de negócio centralizadas num só lugar.
    private static void Validar(Produto produto)
    {
        if (string.IsNullOrWhiteSpace(produto.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (produto.Preco < 0)
            throw new ArgumentException("O preço não pode ser negativo.");

        if (produto.Estoque < 0)
            throw new ArgumentException("O estoque não pode ser negativo.");
    }
}