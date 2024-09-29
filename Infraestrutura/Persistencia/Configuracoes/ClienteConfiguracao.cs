using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class ClienteConfiguracao : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.CpfCnpj).HasMaxLength(20);
        builder.Property(c => c.Telefone).HasMaxLength(20);
        builder.Property(c => c.DataNascimento).IsRequired();
        builder.HasOne(c => c.Endereco)
            .WithOne()
            .HasForeignKey<Cliente>(c => c.EnderecoId);
    }
}
