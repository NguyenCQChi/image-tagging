using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authentication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEndpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
