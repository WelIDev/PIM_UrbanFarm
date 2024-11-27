using Aplicacao.Interfaces;
using Dominio.Interfaces.Repositorios;
using Infraestrutura.Persistencia;
using Infraestrutura.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestrutura.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IVendaRepositorio VendaRepositorio => new VendaRepositorio(_context);

    public IAbastecimentoEstoqueRepositorio AbastecimentoEstoqueRepositorio =>
        new AbastecimentoEstoqueRepositorio(_context);
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        var _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
}