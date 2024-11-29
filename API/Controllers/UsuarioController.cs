using Aplicacao.DTOs;
using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PIM_UrbanFarm.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsuarioController : Controller
{
    private readonly IUsuarioServico _usuarioServico;

    public UsuarioController(IUsuarioServico usuarioServico)
    {
        _usuarioServico = usuarioServico;
    }

    [HttpPost]
    public ActionResult InserirUsuario([FromBody] UsuarioDto usuarioDto)
    {
        var usuario = new Usuario
        {
            Nome = usuarioDto.Nome,
            Email = usuarioDto.Email,
            Funcao = (Funcao)usuarioDto.Funcao
        };
        _usuarioServico.InserirUsuario(usuario, usuarioDto.Senha);
        return Ok("Usuario gravado com sucesso!");
    }

    [HttpGet]
    [Authorize]
    public ActionResult ObterPorEmail(string email)
    {
        var usuario = _usuarioServico.ObterPorEmail(email);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpGet]
    public ActionResult ObterPorId(int id)
    {
        var usuario = _usuarioServico.ObterPorId(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpGet]
    public ActionResult ObterUsuarios()
    {
        var usuarios = _usuarioServico.ObterUsuarios();

        if (usuarios.Count == 0)
        {
            return NotFound();
        }

        return Ok(usuarios);
    }

    [HttpDelete]
    public ActionResult ExcluirUsuario(int id)
    {
        var usuario = _usuarioServico.ObterPorId(id);
        if (usuario == null)
        {
            return NotFound("Usuario não encontrado.");
        }
        _usuarioServico.ExcluirUsuario(id);
        return Ok("Usuario excluido com sucesso");
    }
}