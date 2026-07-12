// Repositories/IProdutoRepository.cs
using PetShop.Api.Models;

namespace PetShop.Api.Repositories;

public interface IProdutoRepository
{
    IEnumerable<Produto> Listar();
    Produto? ObterPorId(int id);
    Produto Adicionar(Produto produto);
    bool Atualizar(Produto produto);
    bool Remover(int id);
}