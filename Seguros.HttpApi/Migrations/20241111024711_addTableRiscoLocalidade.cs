using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seguros.HttpApi.Migrations
{
    /// <inheritdoc />
    public partial class addTableRiscoLocalidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiscoPorLocalidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UF = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    NivelRisco = table.Column<string>(type: "varchar(15)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiscoPorLocalidade", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiscoPorLocalidade_UF_Cidade_Bairro",
                table: "RiscoPorLocalidade",
                columns: new[] { "UF", "Cidade", "Bairro" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiscoPorLocalidade");
        }
    }
}
