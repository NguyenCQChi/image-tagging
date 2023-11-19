using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsersDbCOnstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "LocalUsers",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 16, 10, 51, 72, DateTimeKind.Local).AddTicks(9053));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 16, 10, 51, 72, DateTimeKind.Local).AddTicks(9077));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 16, 5, 39, 427, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 16, 5, 39, 427, DateTimeKind.Local).AddTicks(9229));
        }
    }
}
