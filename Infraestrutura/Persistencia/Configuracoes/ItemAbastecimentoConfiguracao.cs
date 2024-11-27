using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class ItemAbastecimentoConfiguracao : IEntityTypeConfiguration<ItemAbastecimento>
{
    public void Configure(EntityTypeBuilder<ItemAbastecimento> builder)
    {
        builder.HasKey(ia => ia.Id);

        builder.HasOne(ia => ia.Produto).WithMany(p => p.ItensAbastecimento).HasForeignKey(ia =>
            ia.ProdutoId);

        builder.Property(ia => ia.Custo).HasColumnType("decimal(10, 2)");
    }
}
