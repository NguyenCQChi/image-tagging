using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEndpointsAgain2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "20bac131-c0fb-42c4-865e-3d70a976db46", "20bac131-c0fb-42c4-865e-3d70a976db46", "admin", "ADMIN" },
                    { "bf283151-00a3-47dd-894a-9ee86a2aa2eb", "bf283151-00a3-47dd-894a-9ee86a2aa2eb", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "58835548-56c8-48c0-95b7-252ac7c0c6a9", 0, "706e1e1e-cb5a-4d65-a510-f11c01a46d29", "mrandhawa40@my.bcit.ca", false, false, null, "Administrator", "MRANDHAWA40@MY.BCIT.CA", "ADMIN", "AQAAAAIAAYagAAAAEMWcauzUHfo23fR7IAsHjvwTaJysC7YMYNI0MM9JC5BxHthVB9JGD5SDe1Y9vF0LYQ==", null, false, "cc3a4c5b-144a-46cf-bc40-3dff3107dcb6", false, "admin" });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { "12835f88-5094-4e55-ba59-4b86cb267b50", "PATCH" },
                    { "5b230900-c7d5-4b64-a3be-7e899c70aea8", "GET" },
                    { "c8ca8cbe-4f01-40ee-bd36-a32fe87daa65", "POST" },
                    { "fd1a1d5c-d515-472e-b4c4-49cee2e3774a", "DELETE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "20bac131-c0fb-42c4-865e-3d70a976db46", "58835548-56c8-48c0-95b7-252ac7c0c6a9" });

            migrationBuilder.InsertData(
                table: "EndpointTypes",
                columns: new[] { "Id", "Name", "RequestTypeId" },
                values: new object[,]
                {
                    { "05642e45-0542-4ef2-a464-7a84b1a893e5", "/api/v1/auth/allUserInformation", "5b230900-c7d5-4b64-a3be-7e899c70aea8" },
                    { "543ac586-1664-4a6c-8509-b9b8e7c82313", "/api/v1/auth/getAllEndpoints", "5b230900-c7d5-4b64-a3be-7e899c70aea8" },
                    { "6bdbbc95-d4a3-4590-8163-08f6087c47b0", "/api/v1/auth/resetPassword", "12835f88-5094-4e55-ba59-4b86cb267b50" },
                    { "72c0827f-61a4-4ccf-a526-2e7c6c90b4b1", "/api/v1/auth/totalRequestsPerEndpoint", "5b230900-c7d5-4b64-a3be-7e899c70aea8" },
                    { "7686d01a-ae17-4e1c-8754-191bd777e13e", "/api/v1/auth/register", "c8ca8cbe-4f01-40ee-bd36-a32fe87daa65" },
                    { "8bab9616-66fd-4445-8893-f2183fded108", "/api/v1/auth/validate", "c8ca8cbe-4f01-40ee-bd36-a32fe87daa65" },
                    { "a260b1b2-f323-4768-9511-f2da26c1c7f9", "/api/v1/auth/login", "c8ca8cbe-4f01-40ee-bd36-a32fe87daa65" },
                    { "b2298926-1602-414e-8f1b-78ac7642178a", "/api/v1/auth/userInformation/", "5b230900-c7d5-4b64-a3be-7e899c70aea8" },
                    { "f210bab9-9779-4229-9ad3-1093059d1665", "/api/v1/auth/refresh", "c8ca8cbe-4f01-40ee-bd36-a32fe87daa65" },
                    { "f989e13e-d793-4a73-8734-3d4b4ec6a695", "/api/v1/auth/revoke", "fd1a1d5c-d515-472e-b4c4-49cee2e3774a" },
                    { "fd1722cd-2974-49f7-aab2-1538606effd3", "/api/v1/auth/resetPassword/", "5b230900-c7d5-4b64-a3be-7e899c70aea8" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf283151-00a3-47dd-894a-9ee86a2aa2eb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "20bac131-c0fb-42c4-865e-3d70a976db46", "58835548-56c8-48c0-95b7-252ac7c0c6a9" });

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "05642e45-0542-4ef2-a464-7a84b1a893e5");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "543ac586-1664-4a6c-8509-b9b8e7c82313");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "6bdbbc95-d4a3-4590-8163-08f6087c47b0");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "72c0827f-61a4-4ccf-a526-2e7c6c90b4b1");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "7686d01a-ae17-4e1c-8754-191bd777e13e");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "8bab9616-66fd-4445-8893-f2183fded108");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "a260b1b2-f323-4768-9511-f2da26c1c7f9");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "b2298926-1602-414e-8f1b-78ac7642178a");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "f210bab9-9779-4229-9ad3-1093059d1665");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "f989e13e-d793-4a73-8734-3d4b4ec6a695");

            migrationBuilder.DeleteData(
                table: "EndpointTypes",
                keyColumn: "Id",
                keyValue: "fd1722cd-2974-49f7-aab2-1538606effd3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20bac131-c0fb-42c4-865e-3d70a976db46");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58835548-56c8-48c0-95b7-252ac7c0c6a9");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "12835f88-5094-4e55-ba59-4b86cb267b50");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "5b230900-c7d5-4b64-a3be-7e899c70aea8");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "c8ca8cbe-4f01-40ee-bd36-a32fe87daa65");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "Id",
                keyValue: "fd1a1d5c-d515-472e-b4c4-49cee2e3774a");

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
    }
}
