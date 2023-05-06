using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class CreatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a3802374-af73-4deb-ba89-0ca0be5264d2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Birthday", "CreatedBy", "CreatedOn", "Gender", "Login", "ModifiedBy", "ModifiedOn", "Name", "Password", "RevokedBy", "RevokedOn" },
                values: new object[] { new Guid("860ad9ab-87b4-4f58-ba24-21da529d3d4a"), true, null, "Admin", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), 2, "Admin", null, null, "Admin", "Admin123", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("860ad9ab-87b4-4f58-ba24-21da529d3d4a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Birthday", "CreatedBy", "CreatedOn", "Gender", "Login", "ModifiedBy", "ModifiedOn", "Name", "Password", "RevokedBy", "RevokedOn" },
                values: new object[] { new Guid("a3802374-af73-4deb-ba89-0ca0be5264d2"), true, null, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Admin", null, null, "Admin", "Admin123", null, null });
        }
    }
}
