using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b7c0b47-b138-4608-9c59-4240f017272a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a06a9c72-ec23-4ca3-9935-4c146b89242b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72075409-5e64-4a1d-bdc6-a1e5b463ef66", "72075409-5e64-4a1d-bdc6-a1e5b463ef66", "admin", "ADMIN" },
                    { "c202e299-af39-44b9-977f-e6d9666b8e9b", "c202e299-af39-44b9-977f-e6d9666b8e9b", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ebf6ab6d-681d-4786-9002-b871c9318046", 0, "cbc34dc7-9ca5-42c7-a620-7147b8ea1c04", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEEkD/IUiQcQq0HJhNOl0igV1xbDQi4Xk8LvZMiF6BJsHEwsZ7cm1DzcGMroiKFAabw==", null, false, "6f2e9641-1972-422a-b535-8eae696ceb62", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "72075409-5e64-4a1d-bdc6-a1e5b463ef66", "ebf6ab6d-681d-4786-9002-b871c9318046" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c202e299-af39-44b9-977f-e6d9666b8e9b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "72075409-5e64-4a1d-bdc6-a1e5b463ef66", "ebf6ab6d-681d-4786-9002-b871c9318046" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72075409-5e64-4a1d-bdc6-a1e5b463ef66");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ebf6ab6d-681d-4786-9002-b871c9318046");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b7c0b47-b138-4608-9c59-4240f017272a", null, "admin", "ADMIN" },
                    { "a06a9c72-ec23-4ca3-9935-4c146b89242b", null, "user", "USER" }
                });
        }
    }
}
