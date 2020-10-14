using APIWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIWeb.Mapeamento
{
    public class ParametroMap : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.ToTable("Parametro");
            builder.HasKey(chave => chave.IdParametro);
            builder.Property(prop => prop.PercentualComissao).IsRequired().HasColumnType("decimal(14,2)");
            builder.Property(prop => prop.PercentualJuros).IsRequired().HasColumnType("decimal(14,2)");
            builder.Property(prop => prop.TipoJuros).IsRequired().HasColumnType("int");
            builder.Property(prop => prop.QuantidadeParcelaMaxima).IsRequired().HasColumnType("int");
        }
    }
}
