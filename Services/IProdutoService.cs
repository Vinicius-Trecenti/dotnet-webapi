using PetShop.Api.Models;

namespace PetShop.Api.Services;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> ListarAsync();
    Task<Produto?> ObterPorIdAsync(int id);
    Task<Produto> CriarAsync(Produto produto);
    Task<bool> AtualizarAsync(Produto produto);
    Task<bool> RemoverAsync(int id);
}