using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seguros.HttpApi.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Condutores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Residencia_Uf = table.Column<string>(type: "varchar(2)", nullable: false),
                    Residencia_Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Residencia_Bairro = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condutores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Residencia_Uf = table.Column<string>(type: "varchar(2)", nullable: false),
                    Residencia_Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Residencia_Bairro = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apolices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Veiculo_Marca = table.Column<string>(type: "varchar(50)", nullable: false),
                    Veiculo_Modelo = table.Column<string>(type: "varchar(50)", nullable: false),
                    Veiculo_Ano = table.Column<int>(type: "int", nullable: false),
                    Endereco_Uf = table.Column<string>(type: "varchar(2)", nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Endereco_Bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cobertura_RouboFurto = table.Column<bool>(type: "bit", nullable: false),
                    Cobertura_Colisao = table.Column<bool>(type: "bit", nullable: false),
                    Cobertura_Terceiros = table.Column<bool>(type: "bit", nullable: false),
                    Cobertura_Residencial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apolices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apolices_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApoliceCondutor",
                columns: table => new
                {
                    ApoliceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CondutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApoliceCondutor", x => new { x.ApoliceId, x.CondutorId });
                    table.ForeignKey(
                        name: "FK_ApoliceCondutor_Apolices_ApoliceId",
                        column: x => x.ApoliceId,
                        principalTable: "Apolices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApoliceCondutor_Condutores_CondutorId",
                        column: x => x.CondutorId,
                        principalTable: "Condutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApoliceCondutor_CondutorId",
                table: "ApoliceCondutor",
                column: "CondutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Apolices_ProprietarioId",
                table: "Apolices",
                column: "ProprietarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApoliceCondutor");

            migrationBuilder.DropTable(
                name: "Apolices");

            migrationBuilder.DropTable(
                name: "Condutores");

            migrationBuilder.DropTable(
                name: "Proprietarios");
        }
    }
}
