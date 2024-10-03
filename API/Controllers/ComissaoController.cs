using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ComissaoController : Controller
{
    private readonly IComissaoServico _comissaoServico;

    public ComissaoController(IComissaoServico comissaoServico)
    {
        _comissaoServico = comissaoServico;
    }

    [HttpPost]
    public ActionResult Inserir(Comissao comissao)
    {
        try
        {
            if (_comissaoServico.InserirComissao(comissao))
            {
                return Ok("Comissão incluída com sucesso! Id: " + comissao.Id);
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
            var comissao = _comissaoServico.ObterPorId(id);
            return Ok(comissao);
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
            return StatusCode(500, "Ocorreu um erro ao tentar obter a comissão.");
        }
    }

    [HttpGet]
    public ActionResult ObterComissoes()
    {
        try
        {
            var comissao = _comissaoServico.ObterComissoes();
            return Ok(comissao);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter todas comissões.");
        }
    }

    [HttpPut]
    public ActionResult AlterarComissao(Comissao comissao)
    {
        try
        {
            if (_comissaoServico.AlterarComissao(comissao))
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
    public ActionResult ExcluirComissao(int id)
    {
        try
        {
            if (_comissaoServico.ExcluirComissao(id))
            {
                return Ok();
            }

            return NotFound("Comissão não encontrada.");
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
