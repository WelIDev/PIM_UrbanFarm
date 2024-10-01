using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class MetaController : Controller
{
    private readonly IMetaServico _metaServico;

    public MetaController(IMetaServico metaServico)
    {
        _metaServico = metaServico;
    }

    [HttpPost]
    public ActionResult InserirMeta([FromBody] Meta meta)
    {
        try
        {
            if (_metaServico.InserirMeta(meta))
            {
                return Ok("Meta incluída com sucesso! Id: " + meta.Id);
            }

            return BadRequest("Não foi possível inserir a meta");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir a meta.");
        }
    }
    
    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        try
        {
            var meta = _metaServico.ObterPorId(id);
            return Ok(meta);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("Meta não encontrada.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter a meta.");
        }
    }

    [HttpGet]
    public ActionResult ObterMetas()
    {
        try
        {
            var metas = _metaServico.ObterMetas();
            return Ok(metas);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Meta não encontrada");
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter todas metas.");
        }
    }

    [HttpPut]
    public ActionResult AlterarMeta(Meta meta)
    {
        try
        {
            if (_metaServico.AlterarMeta(meta))
            {
                return Ok();
            }

            return BadRequest("Não foi possível alterar a meta.");
        }
        catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar alterar a meta.");
        }
    }

    [HttpDelete]
    public ActionResult ExcluirProduto(int id)
    {
        try
        {
            if (_metaServico.ExcluirMeta(id))
            {
                return Ok();
            }

            return NotFound("Meta não encontrada.");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar excluir a meta.");
        }
    }
}
