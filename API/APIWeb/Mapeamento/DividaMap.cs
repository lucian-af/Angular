using APIWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIWeb.Mapeamento
{
    public class DividaMap : IEntityTypeConfiguration<Divida>
    {
        public void Configure(EntityTypeBuilder<Divida> builder)
        {
            builder.ToTable("Divida");
            builder.HasKey(chave => chave.IdDivida);            
            builder.Property(prop => prop.IdCliente).IsRequired().HasColumnName("IdCliente");
            builder.Property(prop => prop.DataVencimento).IsRequired().HasColumnType("date");
            builder.Property(prop => prop.QuantidadeParcela).IsRequired();
            builder.Property(prop => prop.ValorOriginal).IsRequired().HasColumnType("decimal(14,2)");
        }
    }
}