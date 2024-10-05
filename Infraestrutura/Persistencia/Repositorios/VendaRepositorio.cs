using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

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
        return _context.Vendas.ToList();
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
