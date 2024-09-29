using Dominio.Entidades;

namespace Dominio.Interfaces.Repositorios;

public interface IClienteRepositorio
{
    void InserirCliente(Cliente cliente);
    Cliente? ObterPorId(int id);
    List<Cliente> ObterClientes();
    void ExcluirCliente(Cliente cliente);
    void AtualizarCliente(Cliente cliente);
}