using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerraSenseApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_RELATORIO_PLANTACAO",
                columns: table => new
                {
                    ID_RELATORIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_PLANTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NomePlantacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NomePropriedade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ndvi = table.Column<decimal>(type: "DECIMAL(4,2)", precision: 4, scale: 2, nullable: false),
                    StatusGeral = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Temperatura = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    Umidade = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    Chuva = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    RadiacaoSolar = table.Column<decimal>(type: "DECIMAL(8,2)", precision: 8, scale: 2, nullable: false),
                    DataRelatorio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RELATORIO_PLANTACAO", x => x.ID_RELATORIO);
                });

            migrationBuilder.CreateTable(
                name: "TB_OBSERVACAO_RELATORIO",
                columns: table => new
                {
                    ID_OBSERVACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DS_OBSERVACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ID_RELATORIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_OBSERVACAO_RELATORIO", x => x.ID_OBSERVACAO);
                    table.ForeignKey(
                        name: "FK_TB_OBSERVACAO_RELATORIO_TB_RELATORIO_PLANTACAO_ID_RELATORIO",
                        column: x => x.ID_RELATORIO,
                        principalTable: "TB_RELATORIO_PLANTACAO",
                        principalColumn: "ID_RELATORIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_OBSERVACAO_RELATORIO_ID_RELATORIO",
                table: "TB_OBSERVACAO_RELATORIO",
                column: "ID_RELATORIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_OBSERVACAO_RELATORIO");

            migrationBuilder.DropTable(
                name: "TB_RELATORIO_PLANTACAO");
        }
    }
}
