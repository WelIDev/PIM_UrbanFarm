using Aplicacao.Interfaces;
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Infraestrutura.Servicos;

public class SegurancaServico : ISegurancaServico
{
    private readonly IPasswordHasher<Usuario> _passawordHasher;

    public SegurancaServico(IPasswordHasher<Usuario> passawordHasher)
    {
        _passawordHasher = passawordHasher;
    }

    public string HashSenha(Usuario usuario, string senha)
    {
        return _passawordHasher.HashPassword(usuario, senha);
    }

    public bool VerificarSenha(Usuario usuario, string senha)
    {
        var resultado = _passawordHasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
        return resultado == PasswordVerificationResult.Success;
    }
}