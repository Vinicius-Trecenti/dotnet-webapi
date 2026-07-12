// Controllers/ProdutosController.cs
using Microsoft.AspNetCore.Mvc;
using PetShop.Api.Models;
using PetShop.Api.Services;

namespace PetShop.Api.Controllers;

[ApiController]
[Route("[controller]")]   // vira /produtos
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    // O Service chega pronto aqui — injeção de dependência de novo.
    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    // GET /produtos
    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_service.Listar());
    }

    // GET /produtos/5
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var produto = _service.ObterPorId(id);
        if (produto is null)
            return NotFound();   // 404

        return Ok(produto);      // 200
    }

    // POST /produtos
    [HttpPost]
    public IActionResult Criar(Produto produto)
    {
        try
        {
            var criado = _service.Criar(produto);
            // 201 + o header Location apontando pro recurso novo
            return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);   // 400 — regra de negócio violada
        }
    }

    // PUT /produtos/5
    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Produto produto)
    {
        produto.Id = id;   // o id vem da URL, não do corpo
        try
        {
            if (!_service.Atualizar(produto))
                return NotFound();

            return NoContent();   // 204 — deu certo, sem corpo de resposta
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /produtos/5
    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        if (!_service.Remover(id))
            return NotFound();

        return NoContent();   // 204
    }
}