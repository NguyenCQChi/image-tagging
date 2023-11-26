using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddTableUserEndpointRequestsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06606f4d-d4b9-45a5-958d-d26787bedae7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "98193fe7-851b-413c-9bf4-77633f12087d", "64fa5799-ed15-4592-bc6e-7bfef99968b8" });

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "10697569-26db-4ade-a75d-679c71e56476");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "11859f34-2741-4352-a476-7db0bfa5fc62");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "2a741ab7-70d8-493c-ac98-f54e3792878e");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "3f040644-db79-4b40-84e7-db7b54643561");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "60a79e01-1ace-439d-899d-b7f75d14056d");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "849da0e0-6bca-4044-a1c8-7bf6f09c8de7");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "880b6aca-1521-4390-b173-3766bddbe9e6");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "b91baa09-1eed-4e37-a8fc-c174b6a5b744");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "f54f07c6-8f77-49e6-9d66-6c1d4526f155");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "f84b63e7-b787-44a2-a91c-9ac4e2c7806f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98193fe7-851b-413c-9bf4-77633f12087d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64fa5799-ed15-4592-bc6e-7bfef99968b8");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "088ad7c5-0de4-416a-bc0f-5e9d6ba7d7ca");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "1341b0da-9ef7-4b98-9e55-5dbf66328ac9");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "4f9c19e3-7d52-4797-9ae6-9b1868373bdb");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "a3c92bad-3ab9-479a-bbbb-637a136e555f");

            migrationBuilder.CreateTable(
                name: "UserEndpointRequests",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    EndpointTypeId = table.Column<string>(type: "varchar(255)", nullable: false),
                    NumRequests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEndpointRequests", x => new { x.UserId, x.EndpointTypeId });
                    table.ForeignKey(
                        name: "FK_UserEndpointRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEndpointRequests_EndpointTypes_EndpointTypeId",
                        column: x => x.EndpointTypeId,
                        principalTable: "EndpointTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42c4acf1-bfa1-4549-bb9a-2836477373e9", "42c4acf1-bfa1-4549-bb9a-2836477373e9", "user", "USER" },
                    { "ca6a6c9c-a603-4ea5-93bc-6640526491d0", "ca6a6c9c-a603-4ea5-93bc-6640526491d0", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "72151f47-df50-4ddb-aee4-2be5dded36a6", 0, "ad32e151-ac24-486f-a0ce-5ca898c52954", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEP08MIq7qfO9as5yKJsz9y6UQMmlRlpz8bMHiv/wGuXiHQrT+4kAdLiTC3Sb0p3BQQ==", null, false, "74f1d4b4-1e28-4169-b3e5-882d98940baa", false, "admin" });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { "0964c943-ee5e-4cac-bc26-31d780ad2203", "PATCH" },
                    { "3a41c695-a8ef-437c-843d-fca889ca275c", "GET" },
                    { "9e4d9e5e-b0a1-442a-9396-62f90dfc20ed", "DELETE" },
                    { "edc3c314-7b15-4200-8717-d79686bcb8e1", "POST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ca6a6c9c-a603-4ea5-93bc-6640526491d0", "72151f47-df50-4ddb-aee4-2be5dded36a6" });

            migrationBuilder.InsertData(
                table: "EndpointTypes",
                columns: new[] { "Id", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { "08da49c5-5d57-4fc3-8024-e0560c19fbf6", "/api/v1/auth/login", "edc3c314-7b15-4200-8717-d79686bcb8e1" },
                    { "3aa9d8b4-3841-4faa-ac38-23e5216e9c84", "/api/v1/auth/resetPassword", "0964c943-ee5e-4cac-bc26-31d780ad2203" },
                    { "4113a2c4-8cc4-450c-8169-d6620bdb954d", "/api/v1/auth/validate", "edc3c314-7b15-4200-8717-d79686bcb8e1" },
                    { "4d74d91d-b12c-4f0d-84d0-3af146a4f48a", "/api/v1/auth/userInformation", "3a41c695-a8ef-437c-843d-fca889ca275c" },
                    { "50cdfa01-339c-466b-94eb-bb52ce2233cb", "/api/v1/auth/revoke", "9e4d9e5e-b0a1-442a-9396-62f90dfc20ed" },
                    { "75b1ca7d-5fbb-4e23-b3cb-3bed2a734ddc", "/api/v1/auth/register", "edc3c314-7b15-4200-8717-d79686bcb8e1" },
                    { "847d05af-f550-4428-b0c2-28aa5f43f7b1", "/api/v1/auth/resetPassword", "3a41c695-a8ef-437c-843d-fca889ca275c" },
                    { "e6510111-de3f-4a6a-b38c-7aff501b1dea", "/api/v1/auth/refresh", "edc3c314-7b15-4200-8717-d79686bcb8e1" },
                    { "fbe5fccd-ba35-4568-acc8-9cc3d053d827", "/api/v1/auth/allUserInformation", "3a41c695-a8ef-437c-843d-fca889ca275c" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEndpointRequests_EndpointTypeId",
                table: "UserEndpointRequests",
                column: "EndpointTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEndpointRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42c4acf1-bfa1-4549-bb9a-2836477373e9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ca6a6c9c-a603-4ea5-93bc-6640526491d0", "72151f47-df50-4ddb-aee4-2be5dded36a6" });

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "08da49c5-5d57-4fc3-8024-e0560c19fbf6");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "3aa9d8b4-3841-4faa-ac38-23e5216e9c84");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "4113a2c4-8cc4-450c-8169-d6620bdb954d");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "4d74d91d-b12c-4f0d-84d0-3af146a4f48a");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "50cdfa01-339c-466b-94eb-bb52ce2233cb");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "75b1ca7d-5fbb-4e23-b3cb-3bed2a734ddc");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "847d05af-f550-4428-b0c2-28aa5f43f7b1");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "e6510111-de3f-4a6a-b38c-7aff501b1dea");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "fbe5fccd-ba35-4568-acc8-9cc3d053d827");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca6a6c9c-a603-4ea5-93bc-6640526491d0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72151f47-df50-4ddb-aee4-2be5dded36a6");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "0964c943-ee5e-4cac-bc26-31d780ad2203");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "3a41c695-a8ef-437c-843d-fca889ca275c");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "9e4d9e5e-b0a1-442a-9396-62f90dfc20ed");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "edc3c314-7b15-4200-8717-d79686bcb8e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06606f4d-d4b9-45a5-958d-d26787bedae7", "06606f4d-d4b9-45a5-958d-d26787bedae7", "user", "USER" },
                    { "98193fe7-851b-413c-9bf4-77633f12087d", "98193fe7-851b-413c-9bf4-77633f12087d", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "64fa5799-ed15-4592-bc6e-7bfef99968b8", 0, "75af60d9-3ee5-4004-9369-32a1d069a3f2", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEIk8CPhtjfShvON0s7m4SnGM1wGn9KTiy8i2SQeW3njA8rtkqQXSC/tx+AwLSTAWCA==", null, false, "7b06ac49-9320-4374-8e16-d2a39ae6023e", false, "admin" });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { "088ad7c5-0de4-416a-bc0f-5e9d6ba7d7ca", "GET" },
                    { "1341b0da-9ef7-4b98-9e55-5dbf66328ac9", "POST" },
                    { "4f9c19e3-7d52-4797-9ae6-9b1868373bdb", "DELETE" },
                    { "a3c92bad-3ab9-479a-bbbb-637a136e555f", "PATCH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "98193fe7-851b-413c-9bf4-77633f12087d", "64fa5799-ed15-4592-bc6e-7bfef99968b8" });

            migrationBuilder.InsertData(
                table: "EndpointTypes",
                columns: new[] { "Id", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { "10697569-26db-4ade-a75d-679c71e56476", "/api/v1/auth/login", "1341b0da-9ef7-4b98-9e55-5dbf66328ac9" },
                    { "11859f34-2741-4352-a476-7db0bfa5fc62", "/api/v1/auth/resetPassword", "088ad7c5-0de4-416a-bc0f-5e9d6ba7d7ca" },
                    { "2a741ab7-70d8-493c-ac98-f54e3792878e", "/api/v1/auth/register", "1341b0da-9ef7-4b98-9e55-5dbf66328ac9" },
                    { "3f040644-db79-4b40-84e7-db7b54643561", "/api/v1/auth/validate", "1341b0da-9ef7-4b98-9e55-5dbf66328ac9" },
                    { "60a79e01-1ace-439d-899d-b7f75d14056d", "/api/v1/auth/allUserInformation", "088ad7c5-0de4-416a-bc0f-5e9d6ba7d7ca" },
                    { "849da0e0-6bca-4044-a1c8-7bf6f09c8de7", "/api/v1/health", "088ad7c5-0de4-416a-bc0f-5e9d6ba7d7ca" },
                    { "880b6aca-1521-4390-b173-3766bddbe9e6", "/api/v1/auth/refresh", "1341b0da-9ef7-4b98-9e55-5dbf66328ac9" },
                    { "b91baa09-1eed-4e37-a8fc-c174b6a5b744", "/api/v1/auth/userInformation", "088ad7c5-0de4-416a-bc0f-5e9d6ba7d7ca" },
                    { "f54f07c6-8f77-49e6-9d66-6c1d4526f155", "/api/v1/auth/revoke", "4f9c19e3-7d52-4797-9ae6-9b1868373bdb" },
                    { "f84b63e7-b787-44a2-a91c-9ac4e2c7806f", "/api/v1/auth/resetPassword", "a3c92bad-3ab9-479a-bbbb-637a136e555f" }
                });
        }
    }
}
