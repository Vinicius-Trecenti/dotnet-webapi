using Microsoft.AspNetCore.Mvc;
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
        return Ok(await _service.ListarAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var produto = await _service.ObterPorIdAsync(id);
        if (produto is null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Produto produto)
    {
        try
        {
            var criado = await _service.CriarAsync(produto);
            return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Produto produto)
    {
        produto.Id = id;
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
}