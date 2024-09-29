using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IUsuarioServico
{
    bool InserirUsuario(Usuario usuario, string senha);
    Usuario? ObterPorEmail(string email);
    Usuario? ObterPorId(int id);
    List<Usuario> ObterUsuarios();
    bool ExcluirUsuario(int id);
}