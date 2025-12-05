using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;

namespace WebShop.Infra.Persistence.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            // Product entity configuration
            modelBuilder
            .HasKey(x => x.Id);

            modelBuilder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Property(x => x.Price)
                     .HasColumnType("decimal(8,2)");

            //Relationship betweeen product and category
            modelBuilder.HasOne(x => x.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(x => x.ProductCategoryId);
            // HasData
            modelBuilder.HasData(

                    new Product
                    {
                        Id = 1,
                        ProductName = "Laptop",
                        Price = 899.99m,
                        ProductCategoryId = 1,
                        ImageUrl = "~/assets/imgs/shop/1.jpg"

                    },
                    new Product
                    {
                        Id = 2,
                        ProductName = "Wireless Mouse",
                        Price = 29.99m,
                        ProductCategoryId = 1,
                        ImageUrl = "~/assets/imgs/shop/2.jpg"
                    },
                    new Product
                    {
                        Id = 3,
                        ProductName = "Office Chair",
                        Price = 149.50m,
                        ProductCategoryId = 2,
                        ImageUrl = "~/assets/imgs/shop/3.jpg"
                    },
                    new Product
                    {
                        Id = 4,
                        ProductName = "Webcam HD 1080p",
                        Price = 45.00m,
                        ProductCategoryId = 1,
                        ImageUrl = "~/assets/imgs/shop/4.jpg"
                    },
                    new Product
                    {
                        Id = 5,
                        ProductName = "Portable SSD 1TB",
                        Price = 129.00m,
                        ProductCategoryId = 1,
                        ImageUrl = "~/assets/imgs/shop/5.jpg"
                    },

                    // Furniture category
                    new Product
                    {
                        Id = 6,
                        ProductName = "Standing Desk",
                        Price = 299.99m,
                        ProductCategoryId = 2,
                        ImageUrl = "~/assets/imgs/shop/6.jpg"
                    },
                    new Product
                    {
                        Id = 7,
                        ProductName = "Bookshelf Wooden",
                        Price = 89.99m,
                        ProductCategoryId = 2,
                        ImageUrl = "~/assets/imgs/shop/7.jpg"
                    },
                    new Product
                    {
                        Id = 8,
                        ProductName = "Table Lamp LED",
                        Price = 25.99m,
                        ProductCategoryId = 3,
                        ImageUrl = "~/assets/imgs/shop/8.jpg"
                    },
                    new Product
                    {
                        Id = 9,
                        ProductName = "Comfort Footrest",
                        Price = 19.99m,
                        ProductCategoryId = 3,
                        ImageUrl = "~/assets/imgs/shop/9.jpg"
                    }
            );
        }
    }
}
