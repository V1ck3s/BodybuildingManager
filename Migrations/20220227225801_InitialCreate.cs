using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodybuildingManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    MotDePasse = table.Column<string>(type: "TEXT", nullable: false),
                    ProgrammeActuelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programmes_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    ProgrammeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seances_Programmes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programmes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciceSeances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExerciceId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SeanceId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreSerie = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreRepetition = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciceSeances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciceSeances_Exercices_ExerciceId",
                        column: x => x.ExerciceId,
                        principalTable: "Exercices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExerciceSeances_Seances_SeanceId",
                        column: x => x.SeanceId,
                        principalTable: "Seances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_ProgrammeActuelId",
                table: "Comptes",
                column: "ProgrammeActuelId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciceSeances_ExerciceId",
                table: "ExerciceSeances",
                column: "ExerciceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciceSeances_SeanceId",
                table: "ExerciceSeances",
                column: "SeanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_CompteId",
                table: "Programmes",
                column: "CompteId");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_ProgrammeId",
                table: "Seances",
                column: "ProgrammeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comptes_Programmes_ProgrammeActuelId",
                table: "Comptes",
                column: "ProgrammeActuelId",
                principalTable: "Programmes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comptes_Programmes_ProgrammeActuelId",
                table: "Comptes");

            migrationBuilder.DropTable(
                name: "ExerciceSeances");

            migrationBuilder.DropTable(
                name: "Exercices");

            migrationBuilder.DropTable(
                name: "Seances");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Comptes");
        }
    }
}
