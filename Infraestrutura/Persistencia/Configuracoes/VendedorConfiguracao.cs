using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class VendedorConfiguracao : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Nome).IsRequired().HasMaxLength(100);
        builder.Property(v => v.Salario);
        builder.Property(v => v.CpfCnpj).HasMaxLength(14);
        builder.Property(v => v.Telefone).HasMaxLength(15);
        builder.Property(v => v.DataContratacao);

        builder.HasOne(v => v.Endereco).WithOne().HasForeignKey<Vendedor>(v => v.EnderecoId);
    }
}
