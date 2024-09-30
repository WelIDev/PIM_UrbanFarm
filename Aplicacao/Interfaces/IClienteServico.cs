using Dominio.Entidades;

namespace Aplicacao.Interfaces;

public interface IClienteServico
{
    bool InserirCliente(Cliente cliente);
    Cliente ObterPorId(int id);
    List<Cliente> ObterClientes();
    bool ExcluirCliente(int id);
    bool AtualizarCliente(Cliente cliente);
}