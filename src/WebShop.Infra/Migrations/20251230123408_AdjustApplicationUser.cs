using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVendor",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 29, 12, 34, 8, 421, DateTimeKind.Utc).AddTicks(2634));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 28, 12, 34, 8, 421, DateTimeKind.Utc).AddTicks(2642));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2025, 12, 29, 12, 34, 8, 421, DateTimeKind.Utc).AddTicks(2643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 22, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(729));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 30, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(790));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 27, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 28, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(797));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 24, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(801));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 20, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(803));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 28, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(806));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 20, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(816));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateUtc",
                value: new DateTime(2025, 12, 20, 13, 34, 8, 422, DateTimeKind.Local).AddTicks(818));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVendor",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
