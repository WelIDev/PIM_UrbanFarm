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
        builder.Property(v => v.Valor);
        builder.Property(v => v.FormaDePagamento);

        builder.HasOne(v => v.Vendedor).WithMany(v => v.Vendas).HasForeignKey(v => v.VendedorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Cliente).WithMany(c => c.Vendas).HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.Produtos).WithMany(p => p.Vendas)
            .UsingEntity<Dictionary<string, object>>("VendaProduto",
                j => j.HasOne<Produto>().WithMany().HasForeignKey
                    ("ProdutoId"),
                j => j.HasOne<Venda>().WithMany().HasForeignKey("VendaId"));
    }
}