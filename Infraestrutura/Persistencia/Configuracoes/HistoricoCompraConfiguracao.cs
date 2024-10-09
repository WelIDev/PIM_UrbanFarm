using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class HistoricoCompraConfiguracao : IEntityTypeConfiguration<HistoricoCompra>
{
    public void Configure(EntityTypeBuilder<HistoricoCompra> builder)
    {
        builder.HasKey(h => h.Id);

        builder.HasOne(h => h.Cliente).WithOne(c => c.HistoricoCompra)
            .HasForeignKey<HistoricoCompra>(h => h.ClienteId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.Vendas).WithOne(v => v.HistoricoCompra).HasForeignKey(v => v
            .HistoricoCompraId).OnDelete(DeleteBehavior.NoAction);
    }
}
