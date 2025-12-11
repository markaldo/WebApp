using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using WebShop.Infra.Persistence.Configuration;



namespace WebShop.Infra.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }    
        public DbSet<ProductCategory> Categories { get; set; }

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderLine> OrderLines => Set<OrderLine>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Call your combined configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
           // modelBuilder.ApplyConfiguration(new OrderConfig());
           // modelBuilder.ApplyConfiguration(new OrderLineConfig());

        }

    }
}
