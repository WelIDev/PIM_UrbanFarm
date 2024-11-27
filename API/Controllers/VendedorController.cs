using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Dtos;
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
    public ActionResult InserirVendedor(VendedorInserirDto vendedorDto)
    {
        var vendedor = new VendedorInserirDto
        {
            Nome = vendedorDto.Nome,
            Salario = vendedorDto.Salario,
            CpfCnpj = vendedorDto.CpfCnpj,
            Telefone = vendedorDto.Telefone,
            DataContratacao = vendedorDto.DataContratacao,
            Endereco = new Endereco
            {
                Rua = vendedorDto.Endereco.Rua,
                Bairro = vendedorDto.Endereco.Bairro,
                Cidade = vendedorDto.Endereco.Cidade,
                Estado = vendedorDto.Endereco.Estado,
                Cep = vendedorDto.Endereco.Cep
            }
        };
        try
        {
            if (_vendedorServico.InserirVendedor(vendedor))
            {
                return Ok("Vendedor incluído com sucesso!");
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

    [HttpGet]
    public async Task<IActionResult> ObterVendedoresComVendas([FromQuery] DateTime dataInicio, 
        DateTime dataFim)
    {
        try
        {
            var vendedores =
                await _vendedorServico.ObterVendedoresComVendasAsync(dataInicio, dataFim);
            return Ok(vendedores);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erro interno: {e.Message}");
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
