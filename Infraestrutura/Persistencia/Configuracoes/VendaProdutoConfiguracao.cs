using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes
{
    public class VendaProdutoConfiguracao : IEntityTypeConfiguration<VendaProduto>
    {
        public void Configure(EntityTypeBuilder<VendaProduto> builder)
        {
            builder.HasKey(vp => new { vp.IdVenda, vp.IdProduto });
            
            builder.HasOne(vp => vp.Venda)
                .WithMany(v => v.VendaProdutos)
                .HasForeignKey(vp => vp.IdVenda)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(vp => vp.Produto)
                .WithMany(p => p.VendaProdutos)
                .HasForeignKey(vp => vp.IdProduto)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(vp => vp.Quantidade)
                .IsRequired();
            builder.Property(vp => vp.ValorTotal);
        }
    }
}

