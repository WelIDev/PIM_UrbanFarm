using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ClienteController : Controller
{
    private readonly IClienteServico _clienteServico;

    public ClienteController(IClienteServico clienteServico)
    {
        _clienteServico = clienteServico;
    }

    [HttpPost]
    public ActionResult InserirCliente([FromBody] Cliente cliente)
    {
        var resultado = _clienteServico.InserirCliente(cliente);

        if (resultado == false)
        {
            return BadRequest("Ocorreu um erro ao incluir o cliente");
        }
        return Ok("Cliente incluído com sucesso! Id: " + cliente.Id);
    }

    [HttpGet]
    public ActionResult ObterClientePorId(int id)
    {
        var resultado = _clienteServico.ObterPorId(id);

        return Ok(resultado);
    }

    [HttpGet]
    public ActionResult ObterClientes()
    {
        return Ok(_clienteServico.ObterClientes());
    }

    [HttpPut]
    public ActionResult AtualizarCliente([FromBody] Cliente cliente)
    {
        if (!_clienteServico.AtualizarCliente(cliente))
        {
            return BadRequest("Erro ao atualizar os dados do cliente.");
        }

        return Ok("Dados atualizados com sucesso.");
    }

    [HttpDelete]
    public ActionResult ExcluirCliente(int id)
    {
        if (!_clienteServico.ExcluirCliente(id))
        {
            return BadRequest("Erro ao excluir o cliente");
        }

        return Ok("Cliente excluído com sucesso");
    }
}