using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddDataMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Badge",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "OrderLines",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Quantity", "SubTotal" },
                values: new object[] { 30, 0m });

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubTotal",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Quantity", "SubTotal" },
                values: new object[] { 25, 0m });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 10, 21, 12, 46, 527, DateTimeKind.Utc).AddTicks(6231));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 9, 21, 12, 46, 527, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2025, 12, 10, 21, 12, 46, 527, DateTimeKind.Utc).AddTicks(6241));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 0, new DateTime(2025, 12, 3, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3347), 499.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 1, new DateTime(2025, 12, 11, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3543), 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 0, new DateTime(2025, 12, 8, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3548), 149.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 0, new DateTime(2025, 12, 9, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3551), 45.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 2, new DateTime(2025, 12, 5, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3555), 99.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 1, new DateTime(2025, 12, 1, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3559), 249.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 0, new DateTime(2025, 12, 9, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3562), 89.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 0, new DateTime(2025, 12, 1, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3565), 25.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Badge", "CreateUtc", "SalePrice" },
                values: new object[] { 0, new DateTime(2025, 12, 1, 22, 12, 46, 528, DateTimeKind.Local).AddTicks(3567), 19.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Badge",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "OrderLines");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderLines",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreateUtc",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
