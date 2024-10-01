using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class MetaRepositorio : IMetaRepositorio
{
    private readonly AppDbContext _context;

    public MetaRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirMeta(Meta meta)
    {
        _context.Metas.Add(meta);
        _context.SaveChanges();
    }

    public Meta? ObterPorId(int id)
    {
        return _context.Metas.Find(id);
    }

    public List<Meta> ObterMetas()
    {
        return _context.Metas.ToList();
    }

    public void AlterarMeta(Meta meta)
    {
        _context.Metas.Update(meta);
        _context.SaveChanges();
    }

    public void ExcluirMeta(Meta meta)
    {
        _context.Metas.Remove(meta);
        _context.SaveChanges();
    }
}
