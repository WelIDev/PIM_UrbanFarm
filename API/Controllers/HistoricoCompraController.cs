using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HistoricoCompraController : Controller
{
    private readonly IHistoricoCompraServico _historicoCompraServico;

    public HistoricoCompraController(IHistoricoCompraServico historicoCompraServico)
    {
        _historicoCompraServico = historicoCompraServico;
    }

    [HttpPost]
    public ActionResult InserirHistorico(HistoricoCompra historicoCompra)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return _historicoCompraServico.InserirHistoricoCompra(historicoCompra)
            ? Ok("Histórico inserido com sucesso.")
            : StatusCode(500, "Ocorreu um erro ao tentar inserir.");
    }

    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        try
        {
            var historicoCompra = _historicoCompraServico.ObterPorId(id);
            return Ok(historicoCompra);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter o histórico.");
        }
    }

    [HttpGet]
    public ActionResult ObterHistoricos()
    {
        try
        {
            var historicoCompras = _historicoCompraServico.ObterHistoricoCompras();
            return Ok(historicoCompras);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter os históricos.");
        }
    }

    [HttpGet("{clienteId}")]
    public async Task<IActionResult> ObterHistoricoPorClienteId(int clienteId)
    {
        try
        {
            var historicoCompraPaginado =
                await _historicoCompraServico.ObterHistoricoCompraPorClienteId(clienteId);
            return Ok(historicoCompraPaginado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    [HttpPut]
    public ActionResult AlterarHistorico(HistoricoCompra historicoCompra)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_historicoCompraServico.AlterarHistoricoCompra(historicoCompra))
            {
                return Ok("Histórico alterado com sucesso.");
            }

            return NotFound("Histórico não encontrada.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar alterar.");
        }
    }

    [HttpDelete]
    public ActionResult ExcluirHistorico(int id)
    {
        try
        {
            if (_historicoCompraServico.ExcluirHistoricoCompra(id))
            {
                return Ok();
            }

            return NotFound("Histórico não encontrada.");
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
