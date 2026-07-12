using Microsoft.AspNetCore.Mvc;
using PetShop.Api.DTOs;
using PetShop.Api.Models;
using PetShop.Api.Services;

namespace PetShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _service.ListarAsync();
        var resposta = produtos.Select(ParaResponse);   // entidade → DTO
        return Ok(resposta);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var produto = await _service.ObterPorIdAsync(id);
        if (produto is null)
            return NotFound();

        return Ok(ParaResponse(produto));
    }

    [HttpPost]
    public async Task<IActionResult> Criar(ProdutoRequest request)
    {
        // DTO de entrada → entidade
        var produto = new Produto
        {
            Nome = request.Nome,
            Preco = request.Preco,
            Estoque = request.Estoque
        };

        try
        {
            var criado = await _service.CriarAsync(produto);
            return CreatedAtAction(nameof(ObterPorId),
                new { id = criado.Id }, ParaResponse(criado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, ProdutoRequest request)
    {
        var produto = new Produto
        {
            Id = id,                    // o Id vem da URL, não do corpo
            Nome = request.Nome,
            Preco = request.Preco,
            Estoque = request.Estoque
        };

        try
        {
            if (!await _service.AtualizarAsync(produto))
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        if (!await _service.RemoverAsync(id))
            return NotFound();

        return NoContent();
    }

    // Método auxiliar: converte a entidade no DTO de saída.
    private static ProdutoResponse ParaResponse(Produto p) => new()
    {
        Id = p.Id,
        Nome = p.Nome,
        Preco = p.Preco,
        Estoque = p.Estoque
    };
}