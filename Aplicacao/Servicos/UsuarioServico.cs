using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacao.Servicos;

public class UsuarioServico : IUsuarioServico
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISegurancaServico _segurançaServico;

    public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, ISegurancaServico segurançaServico)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _segurançaServico = segurançaServico;
    }

    public void InserirUsuario(Usuario usuario, string senha)
    {
        usuario.Senha = _segurançaServico.HashSenha(usuario, senha);
        _usuarioRepositorio.InserirUsuario(usuario);
    }

    public Usuario? ObterPorEmail(string email)
    {
        return _usuarioRepositorio.ObterPorEmail(email);
    }

    public Usuario? ObterPorId(int id)
    {
        return _usuarioRepositorio.ObterPorId(id);
    }

    public List<Usuario> ObterUsuarios()
    {
        return _usuarioRepositorio.ObterUsuarios();
    }

    public void ExcluirUsuario(int id)
    {
        _usuarioRepositorio.ExcluirUsuario(id);
        
    }
}