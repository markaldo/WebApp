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
            modelBuilder.HasKey(c => c.CatergoryId);

            // Name: required, max length 50
            modelBuilder.Property(c => c.CatergoryName)
                        .IsRequired()
                        .HasMaxLength(50);


            // Seed categories
            modelBuilder.HasData(
                new ProductCategory
                {
                    CatergoryId = 1,
                    CatergoryName = "Electronics"
                },
                new ProductCategory
                {
                    CatergoryId = 2,
                    CatergoryName = "Furniture"
                },
                new ProductCategory
                {
                    CatergoryId = 3,
                    CatergoryName = "Accessories"
                }
            );
        }
    }
}
