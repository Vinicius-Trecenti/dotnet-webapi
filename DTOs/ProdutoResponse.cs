// DTOs/ProdutoResponse.cs
namespace PetShop.Api.DTOs;

// O que a API DEVOLVE. Aqui o Id existe, porque o cliente precisa dele.
public class ProdutoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}