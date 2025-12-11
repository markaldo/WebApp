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
    public class OrderLineConfig : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> modelBuilder)
        {
            modelBuilder.HasKey(ol => ol.Id);

            modelBuilder.HasOne(ol => ol.Product)
                .WithMany(p=> p.OrderLines)
                .HasForeignKey(ol => ol.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Property(ol => ol.Quantity)
                .IsRequired();

            // Seed sample OrderLine data.
            // NOTE: Ensure the ProductId and OrderId values below match the IDs of existing seeded Product and Order rows in your database seed.
            modelBuilder.HasData(
                new OrderLine { Id = 1, OrderId = 1, ProductId = 1, Quantity = 30 },
                new OrderLine { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1 },
                new OrderLine { Id = 3, OrderId = 2, ProductId = 1, Quantity = 25 }
             
            );
        }
    }
}
