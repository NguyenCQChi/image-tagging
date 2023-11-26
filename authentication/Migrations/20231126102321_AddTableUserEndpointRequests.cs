using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddTableUserEndpointRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a769734b-b3aa-4f5e-8f48-b1ef59c859c6");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c1d80fb6-f983-46fe-9155-96522df7b902", "0a616edb-e535-4016-80dc-39e10cc4da6f" });

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "038e9c55-fa6e-4c1b-a304-9467ea74152e");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "1f7cf613-47d8-41f5-a758-3e4d2323db2c");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "5fe3f1dc-c504-4698-942e-3baa23ee124e");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "6797cb12-393d-4401-bfb0-026dd6feab7d");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "93501dc3-ba09-4977-9003-bff095b462be");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "add8ce92-1f27-4f17-b581-008454999e23");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "c7135482-91ff-41d2-af53-93916a1bdfd9");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "eee7737b-3a82-40de-ad86-afd32062a560");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "fb342a86-6c3d-4c69-829f-c2f50590fb37");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "fe06c2c0-2e72-4c78-ba40-03019d40ade5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1d80fb6-f983-46fe-9155-96522df7b902");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a616edb-e535-4016-80dc-39e10cc4da6f");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "770d1ba6-32e4-47ac-baa5-9600978d7713");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "84af926a-7e04-44a8-9a40-5c6bca409b0a");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "a8024f19-b44f-4081-a5f9-bddc0528bb32");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "c1276f49-78e0-4bf6-a382-9941025411b1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a769734b-b3aa-4f5e-8f48-b1ef59c859c6", "a769734b-b3aa-4f5e-8f48-b1ef59c859c6", "user", "USER" },
                    { "c1d80fb6-f983-46fe-9155-96522df7b902", "c1d80fb6-f983-46fe-9155-96522df7b902", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0a616edb-e535-4016-80dc-39e10cc4da6f", 0, "59bcdc14-e4ad-4507-b93a-5314b594d597", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEB6taWjGWwdDRiWoY6kBgA9mRpz9blQtvRZB46+GiSjSs3TvBsnpdu0O2DAoOVAumQ==", null, false, "b78b2090-d251-4cfb-addf-0498783a992a", false, "admin" });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { "770d1ba6-32e4-47ac-baa5-9600978d7713", "DELETE" },
                    { "84af926a-7e04-44a8-9a40-5c6bca409b0a", "POST" },
                    { "a8024f19-b44f-4081-a5f9-bddc0528bb32", "PATCH" },
                    { "c1276f49-78e0-4bf6-a382-9941025411b1", "GET" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c1d80fb6-f983-46fe-9155-96522df7b902", "0a616edb-e535-4016-80dc-39e10cc4da6f" });

            migrationBuilder.InsertData(
                table: "EndpointTypes",
                columns: new[] { "Id", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { "038e9c55-fa6e-4c1b-a304-9467ea74152e", "/api/v1/auth/revoke", "770d1ba6-32e4-47ac-baa5-9600978d7713" },
                    { "1f7cf613-47d8-41f5-a758-3e4d2323db2c", "/api/v1/health", "c1276f49-78e0-4bf6-a382-9941025411b1" },
                    { "5fe3f1dc-c504-4698-942e-3baa23ee124e", "/api/v1/auth/login", "84af926a-7e04-44a8-9a40-5c6bca409b0a" },
                    { "6797cb12-393d-4401-bfb0-026dd6feab7d", "/api/v1/auth/resetPassword", "a8024f19-b44f-4081-a5f9-bddc0528bb32" },
                    { "93501dc3-ba09-4977-9003-bff095b462be", "/api/v1/auth/refresh", "84af926a-7e04-44a8-9a40-5c6bca409b0a" },
                    { "add8ce92-1f27-4f17-b581-008454999e23", "/api/v1/auth/allUserInformation", "c1276f49-78e0-4bf6-a382-9941025411b1" },
                    { "c7135482-91ff-41d2-af53-93916a1bdfd9", "/api/v1/auth/validate", "84af926a-7e04-44a8-9a40-5c6bca409b0a" },
                    { "eee7737b-3a82-40de-ad86-afd32062a560", "/api/v1/auth/userInformation", "c1276f49-78e0-4bf6-a382-9941025411b1" },
                    { "fb342a86-6c3d-4c69-829f-c2f50590fb37", "/api/v1/auth/resetPassword", "c1276f49-78e0-4bf6-a382-9941025411b1" },
                    { "fe06c2c0-2e72-4c78-ba40-03019d40ade5", "/api/v1/auth/register", "84af926a-7e04-44a8-9a40-5c6bca409b0a" }
                });
        }
    }
}
