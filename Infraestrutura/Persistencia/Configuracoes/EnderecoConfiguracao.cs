using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class EnderecoConfiguracao : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Rua).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Bairro).HasMaxLength(100);
        builder.Property(e => e.Cidade).HasMaxLength(100);
        builder.Property(e => e.Estado).HasMaxLength(50);
        builder.Property(e => e.Cep).HasMaxLength(20);
    }
}
