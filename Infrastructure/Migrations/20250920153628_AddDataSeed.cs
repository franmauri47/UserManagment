using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Domiciles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Email", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "johnDoe@gmail.com", null, "John Doe" },
                    { 2, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "jSmith@gmail.com", null, "Jane Smith" },
                    { 3, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "aliceJ@gmail.com", null, "Alice Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Domiciles",
                columns: new[] { "Id", "City", "CreationDate", "DirectionNumber", "ModifiedDate", "Province", "Street", "UserId" },
                values: new object[,]
                {
                    { 1, "City X", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "123", null, "Province A", "Main St", 1 },
                    { 2, "City Y", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "456", null, "Province B", "Second St", 2 },
                    { 3, "City Z", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "789", null, "Province C", "Third St", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Domiciles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Domiciles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Domiciles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Domiciles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
