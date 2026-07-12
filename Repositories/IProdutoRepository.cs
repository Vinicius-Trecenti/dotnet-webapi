using PetShop.Api.Models;

namespace PetShop.Api.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> ListarAsync();
    Task<Produto?> ObterPorIdAsync(int id);
    Task<Produto> AdicionarAsync(Produto produto);
    Task<bool> AtualizarAsync(Produto produto);
    Task<bool> RemoverAsync(int id);
}