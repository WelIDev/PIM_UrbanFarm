using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

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

        builder.HasMany(v => v.Produtos)
            .WithMany(p => p.Vendas)
            .UsingEntity<VendaProduto>(
                j => j.HasOne<Produto>()
                    .WithMany()
                    .HasForeignKey(vp => vp.ProdutoId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<Venda>()
                    .WithMany()
                    .HasForeignKey(vp => vp.VendaId)
                    .OnDelete(DeleteBehavior.NoAction),
                j =>
                {
                    j.Property(vp => vp.Quantidade)
                        .HasDefaultValue(1);
                    j.Property(vp => vp.ValorTotal);
                    
                    j.HasKey(vp => new { vp.VendaId, vp.ProdutoId });
                    
                    j.Property(vp => vp.VendaId).HasColumnName("VendaId");
                    j.Property(vp => vp.ProdutoId).HasColumnName("ProdutoId");
                });
    }
}