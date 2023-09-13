using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationsmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppReservas_AppPasajeros_PasajeroId",
                table: "AppReservas");

            migrationBuilder.DropForeignKey(
                name: "FK_AppReservas_AppViajes_ViajeId",
                table: "AppReservas");

            migrationBuilder.AddForeignKey(
                name: "FK_AppReservas_AppPasajeros_PasajeroId",
                table: "AppReservas",
                column: "PasajeroId",
                principalTable: "AppPasajeros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppReservas_AppViajes_ViajeId",
                table: "AppReservas",
                column: "ViajeId",
                principalTable: "AppViajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppReservas_AppPasajeros_PasajeroId",
                table: "AppReservas");

            migrationBuilder.DropForeignKey(
                name: "FK_AppReservas_AppViajes_ViajeId",
                table: "AppReservas");

            migrationBuilder.AddForeignKey(
                name: "FK_AppReservas_AppPasajeros_PasajeroId",
                table: "AppReservas",
                column: "PasajeroId",
                principalTable: "AppPasajeros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppReservas_AppViajes_ViajeId",
                table: "AppReservas",
                column: "ViajeId",
                principalTable: "AppViajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
