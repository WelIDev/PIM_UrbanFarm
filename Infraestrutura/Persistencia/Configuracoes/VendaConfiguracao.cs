using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes
{
    public class VendaConfiguracao : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Data).HasDefaultValueSql("GETDATE()");

            builder.Property(v => v.FormaDePagamento);

            builder.HasOne(v => v.Vendedor)
                .WithMany(v => v.Vendas)
                .HasForeignKey(v => v.VendedorId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(v => v.HistoricoCompra)
                .WithMany(c => c.Vendas)
                .HasForeignKey(v => v.HistoricoCompraId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasMany(v => v.VendaProdutos)
                .WithOne(vp => vp.Venda)
                .HasForeignKey(vp => vp.IdVenda)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
