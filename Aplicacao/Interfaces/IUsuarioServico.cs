using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IUsuarioServico
{
    void InserirUsuario(Usuario usuario, string senha);
    Usuario? ObterPorEmail(string email);
    Usuario? ObterPorId(int id);
    List<Usuario> ObterUsuarios();
    void ExcluirUsuario(int id);
}