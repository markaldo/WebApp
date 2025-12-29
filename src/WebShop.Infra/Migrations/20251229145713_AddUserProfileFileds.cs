using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileFileds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 28, 14, 57, 13, 526, DateTimeKind.Utc).AddTicks(1906));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 27, 14, 57, 13, 526, DateTimeKind.Utc).AddTicks(1915));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2025, 12, 28, 14, 57, 13, 526, DateTimeKind.Utc).AddTicks(1916));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 21, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 29, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8852));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 26, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 27, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 23, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 19, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8864));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 27, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8867));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 19, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 19, 15, 57, 13, 526, DateTimeKind.Local).AddTicks(8872));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 18, 13, 7, 4, 665, DateTimeKind.Utc).AddTicks(6229));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 17, 13, 7, 4, 665, DateTimeKind.Utc).AddTicks(6236));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2025, 12, 18, 13, 7, 4, 665, DateTimeKind.Utc).AddTicks(6238));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 11, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1572));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 19, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1632));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 16, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 17, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1637));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 13, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1639));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 9, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 17, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1645));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 9, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 9, 14, 7, 4, 666, DateTimeKind.Local).AddTicks(1650));
        }
    }
}
