using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebShop.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DeleteProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrl", "Price", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/7.jpg", 45.00m, "Webcam HD 1080p" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrl", "Price", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/8.jpg", 129.00m, "Portable SSD 1TB" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/9.jpg", 299.99m, 2, "Standing Desk" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/10.jpg", 89.99m, 2, "Bookshelf Wooden" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/11.jpg", 25.99m, 3, "Table Lamp LED" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/12.jpg", 19.99m, 3, "Comfort Footrest" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ImageUrl", "Price", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/4.jpg", 199.00m, "Monitor 24 Inch" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ImageUrl", "Price", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/5.jpg", 59.99m, "Keyboard Mechanical" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/5.jpg", 59.99m, 1, "Keyboard Mechanical" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/7.jpg", 45.00m, 1, "Webcam HD 1080p" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/8.jpg", 129.00m, 1, "Portable SSD 1TB" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[] { "~/assets/imgs/shop/9.jpg", 299.99m, 2, "Standing Desk" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateUtc", "ImageUrl", "Price", "ProductCategoryId", "ProductName" },
                values: new object[,]
                {
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/assets/imgs/shop/10.jpg", 89.99m, 2, "Bookshelf Wooden" },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/assets/imgs/shop/11.jpg", 25.99m, 3, "Table Lamp LED" },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "~/assets/imgs/shop/12.jpg", 19.99m, 3, "Comfort Footrest" }
                });
        }
    }
}
