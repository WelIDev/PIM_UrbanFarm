using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class ComissaoRepositorio : IComissaoRepositorio
{
    private readonly AppDbContext _context;

    public ComissaoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirComissao(Comissao comissao)
    {
        _context.Comissoes.Add(comissao);
        _context.SaveChanges();
    }

    public Comissao? ObterPorId(int id)
    {
        return _context.Comissoes.Find(id);
    }

    public List<Comissao> ObterComissoes()
    {
        return _context.Comissoes.ToList();
    }

    public void ExcluirComissao(Comissao comissao)
    {
        _context.Comissoes.Remove(comissao);
        _context.SaveChanges();
    }

    public void AtualizarComissao(Comissao comissao)
    {
        _context.Comissoes.Update(comissao);
        _context.SaveChanges();
    }
}
