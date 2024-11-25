using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class VendaController : Controller
{
    private readonly IVendaServico _vendaServico;

    public VendaController(IVendaServico vendaServico)
    {
        _vendaServico = vendaServico;
    }

    [HttpPost]
    public async Task<ActionResult> InserirVenda([FromBody] VendaDto vendaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var sucesso = await _vendaServico.InserirVenda(vendaDto);
        return sucesso ? Ok("Venda inserida com sucesso.") : StatusCode(500, "Ocorreu um erro ao tentar inserir.");
    }

    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        try
        {
            var venda = _vendaServico.ObterPorId(id);
            return Ok(venda);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter as vendas.");
        }
    }

    [HttpGet]
    public ActionResult ObterVendas()
    {
        try
        {
            var vendas = _vendaServico.ObterVendas();
            return Ok(vendas);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Ocorreu um erro ao tentar obter as vendas. {e.Message}");
        }
    }

    [HttpDelete]
    public ActionResult ExcluirVenda(int id)
    {
        try
        {
            if (_vendaServico.ExcluirVenda(id))
            {
                return Ok();
            }

            return NotFound("Venda não encontrada.");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar excluir.");
        }
    }
}
