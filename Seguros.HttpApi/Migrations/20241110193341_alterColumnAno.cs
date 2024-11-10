using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seguros.HttpApi.Migrations
{
    /// <inheritdoc />
    public partial class alterColumnAno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Veiculo_Ano",
                table: "Apolices",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Veiculo_Ano",
                table: "Apolices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");
        }
    }
}
