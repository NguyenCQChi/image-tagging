using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class RemoveKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 19, 17, 33, 691, DateTimeKind.Local).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 19, 17, 33, 691, DateTimeKind.Local).AddTicks(8705));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 19, 16, 11, 885, DateTimeKind.Local).AddTicks(188));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 14, 19, 16, 11, 885, DateTimeKind.Local).AddTicks(223));
        }
    }
}
