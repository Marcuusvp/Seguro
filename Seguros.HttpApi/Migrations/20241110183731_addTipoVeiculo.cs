using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seguros.HttpApi.Migrations
{
    /// <inheritdoc />
    public partial class addTipoVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Veiculo_Tipo",
                table: "Apolices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Veiculo_Tipo",
                table: "Apolices");
        }
    }
}
