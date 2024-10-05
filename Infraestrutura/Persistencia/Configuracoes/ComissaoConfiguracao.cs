using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Persistencia.Configuracoes;

public class ComissaoConfiguracao : IEntityTypeConfiguration<Comissao>
{
    public void Configure(EntityTypeBuilder<Comissao> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Valor);
        builder.Property(c => c.Data).HasDefaultValueSql("GETDATE()");
        
        builder.HasOne(c => c.Meta).WithMany(m => m.Comissoes).HasForeignKey(c => c.MetaId);
    }
}

