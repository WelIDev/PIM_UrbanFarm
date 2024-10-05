using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class VendedorRepositorio : IVendedorRepositorio
{
    private readonly AppDbContext _context;

    public VendedorRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirVendedor(Vendedor vendedor)
    {
        _context.Vendedores.Add(vendedor);
        _context.SaveChanges();
    }

    public Vendedor? ObterPorId(int id)
    {
        return _context.Vendedores.Find(id);
    }

    public List<Vendedor?> ObterVendedores()
    {
        return _context.Vendedores.ToList();
    }

    public void AlterarVendedor(Vendedor vendedor)
    {
        _context.Vendedores.Update(vendedor);
        _context.SaveChanges();
    }

    public void ExcluirVendedor(Vendedor vendedor)
    {
        _context.Vendedores.Remove(vendedor);
        _context.SaveChanges();
    }
}
