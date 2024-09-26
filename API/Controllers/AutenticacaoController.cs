using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AutenticacaoController
{
    private readonly ITokenServico _tokenServico;
    private readonly IUsuarioServico _usuarioServico;
    private readonly ISegurancaServico _segurancaServico;

    public AutenticacaoController(ITokenServico tokenServico, IUsuarioServico usuarioServico, ISegurancaServico segurancaServico)
    {
        _tokenServico = tokenServico;
        _usuarioServico = usuarioServico;
        _segurancaServico = segurancaServico;
    }

    [HttpPost]
    public ActionResult Login([FromBody] LoginDto loginDto)
    {
        var usuario = _usuarioServico.ObterPorEmail(loginDto.Email);
        if (usuario == null || !_segurancaServico.VerificarSenha(usuario, loginDto.Senha))
        {
            return new UnauthorizedObjectResult(new { message = "Email ou senha inválidos" });
        }
    
        var token = _tokenServico.GerarToken(usuario);
        return new OkObjectResult(token);
    }
}