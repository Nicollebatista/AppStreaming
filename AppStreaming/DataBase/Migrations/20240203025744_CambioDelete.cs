using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class CambioDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerieGeneros_Genres_GeneroId",
                table: "SerieGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_SerieGeneros_Series_SerieId",
                table: "SerieGeneros");

            migrationBuilder.AddForeignKey(
                name: "FK_SerieGeneros_Genres_GeneroId",
                table: "SerieGeneros",
                column: "GeneroId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SerieGeneros_Series_SerieId",
                table: "SerieGeneros",
                column: "SerieId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerieGeneros_Genres_GeneroId",
                table: "SerieGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_SerieGeneros_Series_SerieId",
                table: "SerieGeneros");

            migrationBuilder.AddForeignKey(
                name: "FK_SerieGeneros_Genres_GeneroId",
                table: "SerieGeneros",
                column: "GeneroId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SerieGeneros_Series_SerieId",
                table: "SerieGeneros",
                column: "SerieId",
                principalTable: "Series",
                principalColumn: "Id");
        }
    }
}
