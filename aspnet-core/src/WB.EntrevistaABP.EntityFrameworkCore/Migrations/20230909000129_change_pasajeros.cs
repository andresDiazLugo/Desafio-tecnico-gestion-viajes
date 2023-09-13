using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class changepasajeros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPasajeros_AbpUsers_UsuarioId",
                table: "AppPasajeros");

            migrationBuilder.DropIndex(
                name: "IX_AppPasajeros_UsuarioId",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AppPasajeros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "AppPasajeros",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppPasajeros_UsuarioId",
                table: "AppPasajeros",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPasajeros_AbpUsers_UsuarioId",
                table: "AppPasajeros",
                column: "UsuarioId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
