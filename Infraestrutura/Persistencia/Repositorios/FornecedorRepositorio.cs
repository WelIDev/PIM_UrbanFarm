using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Persistencia.Repositorios;

public class FornecedorRepositorio : IFornecedorRepositorio
{
    private readonly AppDbContext _context;

    public FornecedorRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public void InserirFornecedor(Fornecedor fornecedor)
    {
        _context.Fornecedores.Add(fornecedor);
        _context.SaveChanges();
    }

    public Fornecedor? ObterPorId(int id)
    {
        return _context.Fornecedores.Find(id);
    }

    public List<Fornecedor> ObterFornecedores()
    {
        return _context.Fornecedores.Include(f => f.Endereco).ToList();
    }

    public void AtualizarFornecedor(Fornecedor fornecedor)
    {
        _context.Fornecedores.Update(fornecedor);
        _context.SaveChanges();
    }

    public void ExcluirFornecedor(Fornecedor fornecedor)
    {
        _context.Fornecedores.Remove(fornecedor);
        _context.SaveChanges();
    }
}
