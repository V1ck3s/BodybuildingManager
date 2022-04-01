using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodybuildingManager.Migrations
{
    public partial class AddingPoids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poids",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Kilogramme = table.Column<float>(type: "REAL", nullable: false),
                    DatePoids = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompteId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poids_Comptes_CompteId",
                        column: x => x.CompteId,
                        principalTable: "Comptes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poids_CompteId",
                table: "Poids",
                column: "CompteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poids");
        }
    }
}
