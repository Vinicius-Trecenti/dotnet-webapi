using PetShop.Api.Models;
using PetShop.Api.Repositories;

namespace PetShop.Api.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Produto>> ListarAsync() => _repository.ListarAsync();

    public Task<Produto?> ObterPorIdAsync(int id) => _repository.ObterPorIdAsync(id);

    public async Task<Produto> CriarAsync(Produto produto)
    {
        Validar(produto);
        return await _repository.AdicionarAsync(produto);
    }

    public async Task<bool> AtualizarAsync(Produto produto)
    {
        Validar(produto);
        return await _repository.AtualizarAsync(produto);
    }

    public Task<bool> RemoverAsync(int id) => _repository.RemoverAsync(id);

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