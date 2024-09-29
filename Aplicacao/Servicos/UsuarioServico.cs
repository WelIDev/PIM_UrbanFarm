using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorios;

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

    public bool InserirUsuario(Usuario usuario, string senha)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (string.IsNullOrWhiteSpace(usuario.Nome))
        {
            throw new ArgumentException("Nome do usuário é obrigatório.");
        }

        try
        {
            usuario.Senha = _segurançaServico.HashSenha(usuario, senha);
            _usuarioRepositorio.InserirUsuario(usuario);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir o usuário: " + e.Message);
            return false;
        }
    }

    public Usuario? ObterPorEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email é obrigatório.");
        }

        var usuario = _usuarioRepositorio.ObterPorEmail(email);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        return usuario;
    }

    public Usuario? ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var usuario = _usuarioRepositorio.ObterPorId(id);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        return usuario;
    }

    public List<Usuario> ObterUsuarios()
    {
        var usuarios = _usuarioRepositorio.ObterUsuarios();
        if (usuarios == null || usuarios.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum usuário encontrado.");
        }

        return usuarios;
    }

    public bool ExcluirUsuario(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var usuario = _usuarioRepositorio.ObterPorId(id);
        try
        {
            if (usuario != null)
            {
                _usuarioRepositorio.ExcluirUsuario(usuario);
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar excluir: " + e.Message);
            return false;
        }
    }
}