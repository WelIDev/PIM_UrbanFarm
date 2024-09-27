using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Aplicacao.Servicos;

public class ClienteServico : IClienteServico
{
    private readonly IClienteRepositorio _clienteRepositorio;

    public ClienteServico(IClienteRepositorio clienteRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
    }

    public bool InserirCliente(Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        
        if (string.IsNullOrWhiteSpace(cliente.Nome) || string.IsNullOrWhiteSpace(cliente.Email))
        {
            throw new ArgumentException("Nome e Email são obrigatórios.");
        }
        
        return _clienteRepositorio.InserirCliente(cliente);
    }

    public Cliente ObterPorId(int id)
    {
        return _clienteRepositorio.ObterPorId(id) ?? throw new InvalidOperationException();
    }

    public List<Cliente> ObterClientes()
    {
        return _clienteRepositorio.ObterClientes();
    }

    public bool ExcluirCliente(int id)
    {
        return _clienteRepositorio.ExcluirCliente(id);
    }

    public bool AtualizarCliente(Cliente cliente)
    {
        return _clienteRepositorio.AtualizarCliente(cliente);
    }
}