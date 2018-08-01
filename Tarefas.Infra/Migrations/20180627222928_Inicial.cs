using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tarefas.Infra.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PrimeiroNome = table.Column<string>(maxLength: 50, nullable: false),
                    UltimoNome = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Senha = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListaDeTarefas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaDeTarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaDeTarefas_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "Date", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "Date", nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdListaDeTarefa = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefa_ListaDeTarefas_IdListaDeTarefa",
                        column: x => x.IdListaDeTarefa,
                        principalTable: "ListaDeTarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tarefa_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaDeTarefas_IdUsuario",
                table: "ListaDeTarefas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_IdListaDeTarefa",
                table: "Tarefa",
                column: "IdListaDeTarefa");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_IdUsuario",
                table: "Tarefa",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "ListaDeTarefas");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
