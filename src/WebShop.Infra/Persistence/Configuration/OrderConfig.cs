
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace WebShop.Infra.Persistence.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder.HasKey(o => o.Id);

            modelBuilder.Property(o => o.AdditionalInfo)
                .HasMaxLength(50);

            modelBuilder.HasMany(o => o.OrderLines) // One order -> Many orderline
                .WithOne(ol =>ol.Order)
                .HasForeignKey(ol => ol.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            // Seed sample orders
            modelBuilder.HasData(
                new Order
                {
                    Id = 1,
                    AdditionalInfo = "First seeded order - priority shipping",
                    TotalPrice = 129.99m
                },
                new Order
                {
                    Id = 2,
                    AdditionalInfo = "Second seeded order - gift wrap",
                    TotalPrice = 49.50m
                },
                new Order
                {
                    Id = 3,
                    AdditionalInfo = "Third seeded order - international",
                    TotalPrice = 299.00m
                }
            );
        }
    }
}
