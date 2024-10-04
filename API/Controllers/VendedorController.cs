using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class VendedorController : Controller
{
    private readonly IVendedorServico _vendedorServico;

    public VendedorController(IVendedorServico vendedorServico)
    {
        _vendedorServico = vendedorServico;
    }

    [HttpPost]
    public ActionResult Inserir(Vendedor vendedor)
    {
        try
        {
            if (_vendedorServico.InserirVendedor(vendedor))
            {
                return Ok("Vendedor incluído com sucesso! Id: " + vendedor.Id);
            }

            return BadRequest("Não foi possível incluir");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir");
        }
    }
    
    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        try
        {
            var vendedor = _vendedorServico.ObterPorId(id);
            return Ok(vendedor);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception )
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter o vendedor.");
        }
    }

    [HttpGet]
    public ActionResult ObterVendedores()
    {
        try
        {
            var vendedores = _vendedorServico.ObterVendedores();
            return Ok(vendedores);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter todos vendedores.");
        }
    }

    [HttpPut]
    public ActionResult AlterarVendedor(Vendedor vendedor)
    {
        try
        {
            if (_vendedorServico.AlterarVendedor(vendedor))
            {
                return Ok();
            }

            return BadRequest("Não foi possível alterar");
        }
        catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar alterar.");
        }
    }

    [HttpDelete]
    public ActionResult ExcluirVendedor(int id)
    {
        try
        {
            if (_vendedorServico.ExcluirVendedor(id))
            {
                return Ok();
            }

            return NotFound("Vendedor não encontrada.");
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
