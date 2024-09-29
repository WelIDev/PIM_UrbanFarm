using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IUsuarioRepositorio
{
    void InserirUsuario(Usuario usuario);
    Usuario? ObterPorEmail(string email);
    Usuario? ObterPorId(int id);
    List<Usuario> ObterUsuarios();
    void ExcluirUsuario(Usuario usuario);
}