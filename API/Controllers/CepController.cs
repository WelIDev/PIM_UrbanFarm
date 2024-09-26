using Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CepController : Controller
{
    private readonly IConsultarCepServico _consultarCepServico;

    public CepController(IConsultarCepServico consultarCepServico)
    {
        _consultarCepServico = consultarCepServico;
    }

    [HttpPost]
    public ActionResult ConsultarCep([FromBody] string cep)
    {
        var endereco = _consultarCepServico.Execute(cep);
        return Ok(endereco);
    }
}