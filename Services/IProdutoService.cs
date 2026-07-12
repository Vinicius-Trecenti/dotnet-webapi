// Services/IProdutoService.cs
using PetShop.Api.Models;

namespace PetShop.Api.Services;

public interface IProdutoService
{
    IEnumerable<Produto> Listar();
    Produto? ObterPorId(int id);
    Produto Criar(Produto produto);
    bool Atualizar(Produto produto);
    bool Remover(int id);
}