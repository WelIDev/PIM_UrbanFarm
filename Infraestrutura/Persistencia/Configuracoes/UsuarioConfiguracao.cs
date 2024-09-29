using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class UsuarioConfiguracao : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nome).HasMaxLength(100);
        builder.Property(u => u.Email).HasMaxLength(100);
        builder.Property(u => u.Senha).HasMaxLength(255);
        builder.Property(u => u.Funcao);
        builder.Property(u => u.DataCriacao).HasDefaultValueSql("GETDATE()");
    }
}
