using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IClienteRepositorio
{
    bool InserirCliente(Cliente cliente);
    Cliente? ObterPorId(int id);
    List<Cliente> ObterClientes();
    bool ExcluirCliente(int id);
    bool AtualizarCliente(Cliente cliente);
}