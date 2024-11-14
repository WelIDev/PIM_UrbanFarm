using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Persistencia.Repositorios;

public class VendaRepositorio : IVendaRepositorio
{
    private readonly AppDbContext _context;

    public VendaRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirVenda(Venda venda)
    {
        _context.Vendas.Add(venda);
        _context.SaveChanges();
    }

    public Venda? ObterPorId(int id)
    {
        return _context.Vendas.Find(id);
    }

    public List<Venda> ObterVendas()
    {
        return new List<Venda>(_context.Vendas.Include(v => v.Produtos)).ToList();
    }

    public void AlterarVenda(Venda venda)
    {
        _context.Vendas.Update(venda);
        _context.SaveChanges();
    }

    public void ExcluirVenda(Venda venda)
    {
        _context.Vendas.Remove(venda);
        _context.SaveChanges();
    }
}
