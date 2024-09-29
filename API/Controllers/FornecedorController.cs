using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FornecedorController : Controller
{
    private readonly IFornecedorServico _fornecedorServico;

    public FornecedorController(IFornecedorServico fornecedorServico)
    {
        _fornecedorServico = fornecedorServico;
    }

    [HttpPost]
    public ActionResult Inserir(Fornecedor fornecedor)
    {
        try
        {
            if (_fornecedorServico.Inserir(fornecedor))
            {
                return Ok("Fornecedor incluído com sucesso! Id: " + fornecedor.Id);
            }

            return BadRequest("Não foi possível inserir o fornecedor");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir o fornecedor.");
        }
    }

    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        try
        {
            var fornecedor = _fornecedorServico.ObterPorId(id);
            return Ok(fornecedor);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter o fornecedor.");
        }
    }

    [HttpGet]
    public ActionResult ObterFornecedores()
    {
        try
        {
            var fornecedores = _fornecedorServico.ObterFornecedores();
            return Ok(fornecedores);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar obter todos fornecedores.");
        }
    }

    [HttpPut]
    public ActionResult AlterarFornecedor(Fornecedor fornecedor)
    {
        try
        {
            if (_fornecedorServico.AlterarFornecedor(fornecedor))
            {
                return Ok();
            }

            return BadRequest("Não foi possível alterar o fornecedor");
        }
        catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar alterar o fornecedor.");
        }
    }

    [HttpDelete]
    public ActionResult ExcluirFornecedor(int id)
    {
        try
        {
            if (_fornecedorServico.ExcluirFornecedor(id))
            {
                return Ok();
            }

            return NotFound("Fornecedor não encontrado.");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar excluir o fornecedor.");
        }
    }
}
