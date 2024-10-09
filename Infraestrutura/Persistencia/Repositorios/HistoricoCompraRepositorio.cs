using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class HistoricoCompraRepositorio : IHistoricoCompraRepositorio
{
    private readonly AppDbContext _context;

    public HistoricoCompraRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirHistorico(HistoricoCompra? historicoCompra)
    {
        _context.HistoricoCompras.Add(historicoCompra);
        _context.SaveChanges();
    }

    public HistoricoCompra? ObterPorId(int id)
    {
        return _context.HistoricoCompras.Find(id);
    }

    public List<HistoricoCompra> ObterHistoricosCompras()
    {
        return _context.HistoricoCompras.ToList();
    }

    public void AlterarHistorico(HistoricoCompra historicoCompra)
    {
        _context.HistoricoCompras.Update(historicoCompra);
        _context.SaveChanges();
    }

    public void ExcluirHistorico(HistoricoCompra historicoCompra)
    {
        _context.HistoricoCompras.Remove(historicoCompra);
        _context.SaveChanges();
    }
}
