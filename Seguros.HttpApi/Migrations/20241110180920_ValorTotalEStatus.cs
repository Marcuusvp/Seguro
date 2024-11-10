using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seguros.HttpApi.Migrations
{
    /// <inheritdoc />
    public partial class ValorTotalEStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Apolices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Apolices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Apolices");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Apolices");
        }
    }
}
