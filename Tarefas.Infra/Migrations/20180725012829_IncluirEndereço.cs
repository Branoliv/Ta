using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarefas.Infra.Migrations
{
    public partial class IncluirEndereço : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairoo",
                table: "Usuario",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Usuario",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Usuario",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Usuario",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Usuario",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero_Residencia",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Usuario",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairoo",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Numero_Residencia",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Usuario");
        }
    }
}
