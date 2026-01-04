using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infra.Identity;

namespace WebShop.Infra.Persistence.Configuration
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.User)                  
                   .WithMany(u => u.Addresses)        
                   .HasForeignKey(a => a.UserId)         
                   .IsRequired()                          
                   .OnDelete(DeleteBehavior.Cascade);      

            builder.Property(a => a.UserId).HasMaxLength(450); 
            builder.Property(a => a.AddressLine1).HasMaxLength(200).IsRequired();
            builder.Property(a => a.City).HasMaxLength(100).IsRequired();
            builder.Property(a => a.PostalCode).HasMaxLength(20).IsRequired();
            builder.Property(a => a.Country).HasMaxLength(100).IsRequired();

            builder.HasIndex(a => a.UserId);
            builder.HasIndex(a => new { a.UserId, a.IsDefault });
        }
    }
}
