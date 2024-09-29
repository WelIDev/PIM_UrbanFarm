using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Persistencia.Repositorios;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly AppDbContext _context;

    public ClienteRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
    }

    public Cliente? ObterPorId(int id)
    {
        return _context.Clientes.Find(id);
    }

    public List<Cliente> ObterClientes()
    {
        return _context.Clientes.Include(c => c.Endereco).ToList();
    }

    public void ExcluirCliente(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }

    public void AtualizarCliente(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
    }
}
