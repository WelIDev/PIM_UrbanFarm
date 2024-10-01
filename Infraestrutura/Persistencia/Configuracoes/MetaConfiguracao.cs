using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class MetaConfiguracao : IEntityTypeConfiguration<Meta>
{
    public void Configure(EntityTypeBuilder<Meta> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Descricao).HasMaxLength(255);
        builder.Property(m => m.Valor);
        builder.Property(m => m.Periodo);
        builder.Property(m => m.DataCriacao).HasDefaultValueSql("GETDATE()");
    }
}
