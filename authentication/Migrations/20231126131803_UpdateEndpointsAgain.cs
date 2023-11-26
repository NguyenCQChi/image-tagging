using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEndpointsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9297a99-3e0f-4cac-9162-5564a067dd6a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6bb393a1-b122-4630-90ef-ac489c992305", "7d7efbd2-dfc6-4ee3-ab6e-639f5e476ca6" });

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "294c1d8a-96f0-4e92-896d-c149cff9c361");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "2b8b25bc-7d8e-42f9-947e-1af3b079d916");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "38df833b-a3dc-4622-8cff-2eb663a7bd20");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "5de27e08-1b0d-43e9-961a-cf2b16798e7f");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "617c4251-a50f-49cf-9135-9fd7df3e4f1e");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "8922e6ff-9f0d-4e90-a573-9081d05e1dc1");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "8ca4d48f-8066-44aa-86f6-d69044acfe6b");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "c741e5ac-74cf-4bc0-9a5a-4af6f94baba7");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "f71f966f-c3e4-4b07-86aa-b2846d733b34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bb393a1-b122-4630-90ef-ac489c992305");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7d7efbd2-dfc6-4ee3-ab6e-639f5e476ca6");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "743c5144-c24b-45be-b3a8-c36853f704d9");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "be3dc205-84e4-4e7d-88c3-3d49a8505132");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "c71188d6-53f8-437c-bb25-2687b2932e1f");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "d1de9cce-be07-4618-86b4-c5cb21504425");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2729a057-76ed-4fbb-b6fb-4d969413d4da", "2729a057-76ed-4fbb-b6fb-4d969413d4da", "user", "USER" },
                    { "c8e4df93-49b8-4de2-88a6-79143c0d1d2b", "c8e4df93-49b8-4de2-88a6-79143c0d1d2b", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1c65c000-5889-4f93-bfac-d7b1c38ff884", 0, "f488e234-b595-4352-a37e-dddef9ec0620", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEAPXeWWTxtBmgxBJkKzbABHbdZndPrCqDYzDpeF/qBF/S0ywR4KeWW2tyiRhbTcutg==", null, false, "9341bd56-2d13-4f7c-92bc-a0f1b2e3a79a", false, "admin" });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { "2799d7b9-d64c-41f9-bc1e-d0aaf60b3760", "DELETE" },
                    { "6349b64e-7a2a-4a6f-bed1-598615d139d9", "PATCH" },
                    { "72738927-5ee0-4619-b46a-6d5957586cdd", "GET" },
                    { "82436233-33ed-44c7-bb4b-7b0def0003d5", "POST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c8e4df93-49b8-4de2-88a6-79143c0d1d2b", "1c65c000-5889-4f93-bfac-d7b1c38ff884" });

            migrationBuilder.InsertData(
                table: "EndpointTypes",
                columns: new[] { "Id", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { "2174f21b-4aec-475f-8667-5d9d79939214", "/api/v1/auth/userInformation/", "72738927-5ee0-4619-b46a-6d5957586cdd" },
                    { "22b366cb-6f12-4b88-997d-0dcfde08d7e0", "/api/v1/auth/login", "82436233-33ed-44c7-bb4b-7b0def0003d5" },
                    { "5019a9d9-0a76-4751-92f3-19ea4a4aeccf", "/api/v1/auth/resetPassword", "6349b64e-7a2a-4a6f-bed1-598615d139d9" },
                    { "5378afd6-3ddb-460b-8472-ef07d7edfd29", "/api/v1/auth/validate", "82436233-33ed-44c7-bb4b-7b0def0003d5" },
                    { "82af8cc2-0e93-4697-9313-5f37296d25c0", "/api/v1/auth/register", "82436233-33ed-44c7-bb4b-7b0def0003d5" },
                    { "afd02a2f-5696-4c86-ac64-e39f3a984a20", "/api/v1/auth/resetPassword/", "72738927-5ee0-4619-b46a-6d5957586cdd" },
                    { "b8784d76-f165-4105-a17f-2e0872f4807b", "/api/v1/auth/revoke", "2799d7b9-d64c-41f9-bc1e-d0aaf60b3760" },
                    { "cd237df8-3fa6-40e4-8ce9-df86c8eda798", "/api/v1/auth/allUserInformation", "72738927-5ee0-4619-b46a-6d5957586cdd" },
                    { "d26b3d88-52e6-4b03-b468-9c4652830b70", "/api/v1/auth/refresh", "82436233-33ed-44c7-bb4b-7b0def0003d5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2729a057-76ed-4fbb-b6fb-4d969413d4da");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8e4df93-49b8-4de2-88a6-79143c0d1d2b", "1c65c000-5889-4f93-bfac-d7b1c38ff884" });

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "2174f21b-4aec-475f-8667-5d9d79939214");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "22b366cb-6f12-4b88-997d-0dcfde08d7e0");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "5019a9d9-0a76-4751-92f3-19ea4a4aeccf");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "5378afd6-3ddb-460b-8472-ef07d7edfd29");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "82af8cc2-0e93-4697-9313-5f37296d25c0");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "afd02a2f-5696-4c86-ac64-e39f3a984a20");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "b8784d76-f165-4105-a17f-2e0872f4807b");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "cd237df8-3fa6-40e4-8ce9-df86c8eda798");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "d26b3d88-52e6-4b03-b468-9c4652830b70");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8e4df93-49b8-4de2-88a6-79143c0d1d2b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1c65c000-5889-4f93-bfac-d7b1c38ff884");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "2799d7b9-d64c-41f9-bc1e-d0aaf60b3760");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "6349b64e-7a2a-4a6f-bed1-598615d139d9");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "72738927-5ee0-4619-b46a-6d5957586cdd");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "82436233-33ed-44c7-bb4b-7b0def0003d5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6bb393a1-b122-4630-90ef-ac489c992305", "6bb393a1-b122-4630-90ef-ac489c992305", "admin", "ADMIN" },
                    { "f9297a99-3e0f-4cac-9162-5564a067dd6a", "f9297a99-3e0f-4cac-9162-5564a067dd6a", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7d7efbd2-dfc6-4ee3-ab6e-639f5e476ca6", 0, "f0ba406f-6ea8-4feb-b930-7f0e9af0b7fe", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEPbKwdCRsE9G2/rHoonI07c07e2lsOWZBH+gkvDWtqqZ6VgDrjdEf9zwQjd997ugPA==", null, false, "de26f6bb-6ec4-4e49-bf27-ffaf1bca9eeb", false, "admin" });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { "743c5144-c24b-45be-b3a8-c36853f704d9", "POST" },
                    { "be3dc205-84e4-4e7d-88c3-3d49a8505132", "DELETE" },
                    { "c71188d6-53f8-437c-bb25-2687b2932e1f", "GET" },
                    { "d1de9cce-be07-4618-86b4-c5cb21504425", "PATCH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6bb393a1-b122-4630-90ef-ac489c992305", "7d7efbd2-dfc6-4ee3-ab6e-639f5e476ca6" });

            migrationBuilder.InsertData(
                table: "EndpointTypes",
                columns: new[] { "Id", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { "294c1d8a-96f0-4e92-896d-c149cff9c361", "/api/v1/auth/allUserInformation", "c71188d6-53f8-437c-bb25-2687b2932e1f" },
                    { "2b8b25bc-7d8e-42f9-947e-1af3b079d916", "/api/v1/auth/login", "743c5144-c24b-45be-b3a8-c36853f704d9" },
                    { "38df833b-a3dc-4622-8cff-2eb663a7bd20", "/api/v1/auth/validate", "743c5144-c24b-45be-b3a8-c36853f704d9" },
                    { "5de27e08-1b0d-43e9-961a-cf2b16798e7f", "/api/v1/auth/userInformation/", "c71188d6-53f8-437c-bb25-2687b2932e1f" },
                    { "617c4251-a50f-49cf-9135-9fd7df3e4f1e", "/api/v1/auth/refresh", "743c5144-c24b-45be-b3a8-c36853f704d9" },
                    { "8922e6ff-9f0d-4e90-a573-9081d05e1dc1", "/api/v1/auth/revoke", "be3dc205-84e4-4e7d-88c3-3d49a8505132" },
                    { "8ca4d48f-8066-44aa-86f6-d69044acfe6b", "/api/v1/auth/resetPassword/", "d1de9cce-be07-4618-86b4-c5cb21504425" },
                    { "c741e5ac-74cf-4bc0-9a5a-4af6f94baba7", "/api/v1/auth/register", "743c5144-c24b-45be-b3a8-c36853f704d9" },
                    { "f71f966f-c3e4-4b07-86aa-b2846d733b34", "/api/v1/auth/resetPassword/", "c71188d6-53f8-437c-bb25-2687b2932e1f" }
                });
        }
    }
}
