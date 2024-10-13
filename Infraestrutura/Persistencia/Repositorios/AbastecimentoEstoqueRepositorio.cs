using Dominio.Entidades;
using Dominio.Interfaces.Repositorios;

namespace Infraestrutura.Persistencia.Repositorios;

public class AbastecimentoEstoqueRepositorio : IAbastecimentoEstoqueRepositorio
{
    private readonly AppDbContext _context;

    public AbastecimentoEstoqueRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque)
    {
        _context.AbastecimentosEstoque.Add(abastecimentoEstoque);
        _context.SaveChanges();
    }

    public AbastecimentoEstoque? ObterPorId(int id)
    {
        return _context.AbastecimentosEstoque.Find(id);
    }

    public IList<AbastecimentoEstoque> ObterAbastecimentoEstoque()
    {
        return _context.AbastecimentosEstoque.ToList();
    }

    public void AlterarAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque)
    {
        _context.AbastecimentosEstoque.Update(abastecimentoEstoque);
        _context.SaveChanges();
    }

    public void ExcluirAbastecimentoEstoque(AbastecimentoEstoque abastecimentoEstoque)
    {
        _context.AbastecimentosEstoque.Remove(abastecimentoEstoque);
        _context.SaveChanges();
    }
}
