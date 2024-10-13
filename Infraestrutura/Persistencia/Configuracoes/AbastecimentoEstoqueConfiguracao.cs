using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class AbastecimentoEstoqueConfiguracao : IEntityTypeConfiguration<AbastecimentoEstoque>
{
    public void Configure(EntityTypeBuilder<AbastecimentoEstoque> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Quantidade);
        builder.Property(a => a.Data).HasDefaultValueSql("GETDATE()");
        builder.Property(a => a.Observacoes);

        builder.HasOne(a => a.Produto).WithMany(p => p.AbastecimentosEstoque).HasForeignKey(a =>
            a.ProdutoId);

        builder.HasOne(a => a.Fornecedor).WithMany(f => f.AbastecimentosEstoque).HasForeignKey(a
            => a.FornecedorId);

        builder.HasOne(a => a.Usuario).WithMany(u => u.AbastecimentosEstoque).HasForeignKey(a =>
            a.UsuarioId);
    }
}
