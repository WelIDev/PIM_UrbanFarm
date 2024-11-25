using Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore.Storage;

namespace Aplicacao.Interfaces
{
    public interface IUnitOfWork
    {
        IVendaRepositorio VendaRepositorio { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
