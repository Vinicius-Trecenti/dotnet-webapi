// DTOs/ProdutoRequest.cs
namespace PetShop.Api.DTOs;

// O que o cliente PODE enviar. Repare: não tem Id — ele é do banco.
public class ProdutoRequest
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}