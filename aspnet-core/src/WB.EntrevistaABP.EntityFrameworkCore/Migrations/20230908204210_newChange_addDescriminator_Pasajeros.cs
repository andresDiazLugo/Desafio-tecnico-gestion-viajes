using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class newChangeaddDescriminatorPasajeros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPasajeros_AbpUsers_UsuarioId",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppPasajeros");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AbpUsers");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppPasajeros_Usuarios_Id",
                table: "AppPasajeros",
                column: "Id",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPasajeros_Usuarios_UsuarioId",
                table: "AppPasajeros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPasajeros_Usuarios_Id",
                table: "AppPasajeros");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPasajeros_Usuarios_UsuarioId",
                table: "AppPasajeros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppPasajeros",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppPasajeros",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppPasajeros",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppPasajeros",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppPasajeros",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppPasajeros",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AbpUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPasajeros_AbpUsers_UsuarioId",
                table: "AppPasajeros",
                column: "UsuarioId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
