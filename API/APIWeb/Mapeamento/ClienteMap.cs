using APIWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIWeb.Mapeamento
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(chave => chave.IdCliente);
            builder.Property(prop => prop.Nome).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)");
            builder.Property(prop => prop.Documento).HasMaxLength(14).IsRequired().HasColumnType("varchar(14)");
            builder.Property(prop => prop.EhAtivo).IsRequired().HasColumnType("bit");

            builder.HasMany(cliente => cliente.Dividas)
                .WithOne(divida => divida.Cliente);
        }
    }
}
