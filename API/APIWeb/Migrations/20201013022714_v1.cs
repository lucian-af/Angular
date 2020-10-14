using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIWeb.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Documento = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EhAtivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Parametro",
                columns: table => new
                {
                    IdParametro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantidadeParcelaMaxima = table.Column<int>(type: "int", nullable: false),
                    TipoJuros = table.Column<int>(type: "int", nullable: false),
                    PercentualJuros = table.Column<decimal>(type: "decimal(14,2)", nullable: false),
                    PercentualComissao = table.Column<decimal>(type: "decimal(14,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametro", x => x.IdParametro);
                });

            migrationBuilder.CreateTable(
                name: "Divida",
                columns: table => new
                {
                    IdDivida = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "date", nullable: false),
                    QuantidadeParcela = table.Column<int>(nullable: false),
                    ValorOriginal = table.Column<decimal>(type: "decimal(14,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divida", x => x.IdDivida);
                    table.ForeignKey(
                        name: "FK_Divida_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "IdCliente", "Documento", "EhAtivo", "Nome" },
                values: new object[,]
                {
                    { 1, "12345678900", true, "JOSÉ" },
                    { 2, "00987654321", true, "MARIA" },
                    { 3, "11223344556", true, "PEDRO" },
                    { 4, "67788990011", true, "JOÃO" }
                });

            migrationBuilder.InsertData(
                table: "Parametro",
                columns: new[] { "IdParametro", "PercentualComissao", "PercentualJuros", "QuantidadeParcelaMaxima", "TipoJuros" },
                values: new object[] { 1, 10m, 0.2m, 5, 1 });

            migrationBuilder.InsertData(
                table: "Divida",
                columns: new[] { "IdDivida", "DataVencimento", "IdCliente", "QuantidadeParcela", "ValorOriginal" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 1500m },
                    { 8, new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 9050m },
                    { 2, new DateTime(2020, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 8007m },
                    { 7, new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 2204m },
                    { 3, new DateTime(2020, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 7030m },
                    { 6, new DateTime(2020, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 3702m },
                    { 4, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 6070m },
                    { 5, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 5021m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divida_IdCliente",
                table: "Divida",
                column: "IdCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Divida");

            migrationBuilder.DropTable(
                name: "Parametro");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
