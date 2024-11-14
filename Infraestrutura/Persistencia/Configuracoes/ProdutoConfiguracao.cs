using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Preco);
        builder.Property(p => p.Estoque);
        builder.Property(p => p.Descricao).HasMaxLength(255);

        builder.HasMany(p => p.Fornecedores).WithMany(f => f.Produtos)
            .UsingEntity<Dictionary<string, object>>("FornecedorProduto", j => j
                .HasOne<Fornecedor>().WithMany().HasForeignKey("FornecedorId"), j => j
                .HasOne<Produto>().WithMany().HasForeignKey("ProdutoId"));
        
        builder.HasMany(p => p.VendaProdutos)
            .WithOne(vp => vp.Produto)
            .HasForeignKey(vp => vp.IdProduto) 
            .OnDelete(DeleteBehavior.NoAction);
    }
}
