using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class FornecedorConfiguracao : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Nome).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Email).HasMaxLength(100);
        builder.Property(f => f.Cnpj).HasMaxLength(20);
        builder.Property(f => f.InscricaoEstadual).HasMaxLength(20);
        builder.Property(f => f.Telefone).HasMaxLength(20);
        builder.HasOne(f => f.Endereco)
            .WithOne()
            .HasForeignKey<Fornecedor>(f => f.EnderecoId);
    }
}
