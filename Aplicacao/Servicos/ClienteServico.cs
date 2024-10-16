using Aplicacao.DTOs;
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

    public bool InserirCliente(ClienteDto clienteDto)
    {
        ArgumentNullException.ThrowIfNull(clienteDto);
        if (string.IsNullOrWhiteSpace(clienteDto.Nome) || string.IsNullOrWhiteSpace(clienteDto.Email))
        {
            throw new ArgumentException("Nome e Email são obrigatórios.");
        }
        
        try
        {
            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                CpfCnpj = clienteDto.CpfCnpj,
                Telefone = clienteDto.Telefone,
                Endereco = clienteDto.Endereco
            };
            _clienteRepositorio.InserirCliente(cliente);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar inserir o cliente." + e.Message);
            return false;
        }
    }

    public Cliente ObterPorId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var cliente = _clienteRepositorio.ObterPorId(id);
        if (cliente == null)
        {
            throw new KeyNotFoundException("Cliente não encontrado.");
        }

        return cliente;
    }

    public List<Cliente> ObterClientes()
    {
        var clientes = _clienteRepositorio.ObterClientes();
        if (clientes == null || clientes.Count == 0)
        {
            throw new KeyNotFoundException("Nenhum cliente encontrado.");
        }

        return clientes;
    }

    public bool ExcluirCliente(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID inválido.");
        }

        var cliente = _clienteRepositorio.ObterPorId(id);
        try
        {
            if (cliente != null)
            {
                _clienteRepositorio.ExcluirCliente(cliente);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar excluir: " + e.Message);
            return false;
        }

    }

    public bool AtualizarCliente(Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);

        if (string.IsNullOrWhiteSpace(cliente.Nome))
        {
            throw new ArgumentException("Nome do fornecedor é obrigatório.");
        }

        try
        {
            _clienteRepositorio.AtualizarCliente(cliente);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao tentar alterar: " + e.Message);
            return false;
        }
    }
}