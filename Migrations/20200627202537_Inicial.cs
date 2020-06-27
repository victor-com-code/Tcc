using Microsoft.EntityFrameworkCore.Migrations;

namespace Tcc_Senai.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modalidades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modalidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeCurriculares",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeCurriculares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    IdMod = table.Column<long>(nullable: false),
                    CargaHoraria = table.Column<int>(nullable: false),
                    Sigla = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Modalidades_IdMod",
                        column: x => x.IdMod,
                        principalTable: "Modalidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPerfil = table.Column<long>(nullable: false),
                    IdContrato = table.Column<long>(nullable: false),
                    NomeCompleto = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(maxLength: 20, nullable: false),
                    ConfirmarSenha = table.Column<string>(maxLength: 20, nullable: false),
                    Horario = table.Column<int>(nullable: false),
                    CargaHorariaSemanal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Contratos_IdContrato",
                        column: x => x.IdContrato,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Perfis_IdPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoUnidadeCurriculares",
                columns: table => new
                {
                    IdCurso = table.Column<long>(nullable: false),
                    IdUc = table.Column<long>(nullable: false),
                    CursoId = table.Column<long>(nullable: true),
                    UnidadeCurricularId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoUnidadeCurriculares", x => new { x.IdCurso, x.IdUc });
                    table.ForeignKey(
                        name: "FK_CursoUnidadeCurriculares_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CursoUnidadeCurriculares_UnidadeCurriculares_UnidadeCurricularId",
                        column: x => x.UnidadeCurricularId,
                        principalTable: "UnidadeCurriculares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCurso = table.Column<long>(nullable: false),
                    Modulo = table.Column<int>(nullable: false),
                    Sigla = table.Column<string>(maxLength: 4, nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Semestre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioCursos",
                columns: table => new
                {
                    IdCurso = table.Column<int>(nullable: false),
                    IdFunc = table.Column<int>(nullable: false),
                    CursoId = table.Column<long>(nullable: true),
                    FuncionarioId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioCursos", x => new { x.IdCurso, x.IdFunc });
                    table.ForeignKey(
                        name: "FK_FuncionarioCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuncionarioCursos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_IdMod",
                table: "Cursos",
                column: "IdMod");

            migrationBuilder.CreateIndex(
                name: "IX_CursoUnidadeCurriculares_CursoId",
                table: "CursoUnidadeCurriculares",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoUnidadeCurriculares_UnidadeCurricularId",
                table: "CursoUnidadeCurriculares",
                column: "UnidadeCurricularId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCursos_CursoId",
                table: "FuncionarioCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCursos_FuncionarioId",
                table: "FuncionarioCursos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_IdContrato",
                table: "Funcionarios",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_IdPerfil",
                table: "Funcionarios",
                column: "IdPerfil");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_IdCurso",
                table: "Turmas",
                column: "IdCurso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoUnidadeCurriculares");

            migrationBuilder.DropTable(
                name: "FuncionarioCursos");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "UnidadeCurriculares");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropTable(
                name: "Modalidades");
        }
    }
}
