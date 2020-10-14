using System;
using APIWeb.Mapeamento;
using APIWeb.Models;
using APIWeb.Repository;
using APIWeb.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        #region dbSets
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Divida> Dividas { get; set; }
        public DbSet<Parametro> Parametros { get; set; }        
        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClienteMap());
            builder.ApplyConfiguration(new DividaMap());
            builder.ApplyConfiguration(new ParametroMap());

            InserirDados(builder);
        }
        private static void InserirDados(ModelBuilder builder)
        {
            builder.Entity<Parametro>().HasData(
                new Parametro { IdParametro = 1, TipoJuros = 1, QuantidadeParcelaMaxima = 5, PercentualJuros = 0.2m, PercentualComissao = 10 }
            );

            builder.Entity<Cliente>().HasData(
                new Cliente { IdCliente = 1, Nome = "JOSÉ", Documento = "12345678900", EhAtivo = true },
                new Cliente { IdCliente = 2, Nome = "MARIA", Documento = "00987654321", EhAtivo = true },
                new Cliente { IdCliente = 3, Nome = "PEDRO", Documento = "11223344556", EhAtivo = true },
                new Cliente { IdCliente = 4, Nome = "JOÃO", Documento = "67788990011", EhAtivo = true }
            );

            builder.Entity<Divida>().HasData(
                new Divida { IdDivida = 1, IdCliente = 1, DataVencimento = new DateTime(2020, 07, 13), QuantidadeParcela = 3, ValorOriginal = 1500 },
                new Divida { IdDivida = 2, IdCliente = 2, DataVencimento = new DateTime(2020, 08, 05), QuantidadeParcela = 4, ValorOriginal = 8007 },
                new Divida { IdDivida = 3, IdCliente = 3, DataVencimento = new DateTime(2020, 08, 15), QuantidadeParcela = 5, ValorOriginal = 7030 },
                new Divida { IdDivida = 4, IdCliente = 4, DataVencimento = new DateTime(2020, 09, 07), QuantidadeParcela = 5, ValorOriginal = 6070 },
                new Divida { IdDivida = 5, IdCliente = 4, DataVencimento = new DateTime(2020, 09, 01), QuantidadeParcela = 3, ValorOriginal = 5021 },
                new Divida { IdDivida = 6, IdCliente = 3, DataVencimento = new DateTime(2020, 06, 24), QuantidadeParcela = 5, ValorOriginal = 3702 },
                new Divida { IdDivida = 7, IdCliente = 2, DataVencimento = new DateTime(2020, 06, 25), QuantidadeParcela = 3, ValorOriginal = 2204 },
                new Divida { IdDivida = 8, IdCliente = 1, DataVencimento = new DateTime(2020, 06, 27), QuantidadeParcela = 4, ValorOriginal = 9050 }
            );
        }
        #endregion
    }
}
