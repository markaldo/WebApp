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
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> modelBuilder)
        {
            // Primary Key
            modelBuilder.HasKey(c => c.CategoryId);

            // Name: required, max length 50
            modelBuilder.Property(c => c.CategoryName)
                        .IsRequired()
                        .HasMaxLength(50);


            // Seed categories
            modelBuilder.HasData(
                new ProductCategory
                {
                    CategoryId = 1,
                    CategoryName = "Electronics"
                },
                new ProductCategory
                {
                    CategoryId = 2,
                    CategoryName = "Furniture"
                },
                new ProductCategory
                {
                    CategoryId = 3,
                    CategoryName = "Accessories"
                }
            );
        }
    }
}
