using System.Collections.Generic;
using MegaDesk_Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaDesk_Razor.Data
{
    public class MegaDeskContext : DbContext
    {
        public MegaDeskContext(DbContextOptions<MegaDeskContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ...

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.DeliveryType)
                .WithMany()
                .HasForeignKey(q => q.DeliveryTypeId);

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Material)
                .WithMany()
                .HasForeignKey(q => q.MaterialId);

            // ...
        }

    }
}
