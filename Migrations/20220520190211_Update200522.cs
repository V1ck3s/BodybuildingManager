using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BodybuildingManager.Migrations
{
    public partial class Update200522 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comptes_Programmes_ProgrammeActuelId",
                table: "Comptes");

            migrationBuilder.DropIndex(
                name: "IX_Comptes_ProgrammeActuelId",
                table: "Comptes");

            migrationBuilder.DropColumn(
                name: "ProgrammeActuelId",
                table: "Comptes");

            migrationBuilder.AddColumn<bool>(
                name: "EstActif",
                table: "Programmes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Programmes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstActif",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Programmes");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgrammeActuelId",
                table: "Comptes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_ProgrammeActuelId",
                table: "Comptes",
                column: "ProgrammeActuelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comptes_Programmes_ProgrammeActuelId",
                table: "Comptes",
                column: "ProgrammeActuelId",
                principalTable: "Programmes",
                principalColumn: "Id");
        }
    }
}
