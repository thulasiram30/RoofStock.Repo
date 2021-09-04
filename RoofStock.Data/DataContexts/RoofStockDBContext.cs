using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RoofStock.Data.SeedData;
using RoofStock.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RoofStock.Data.DataContexts
{
    public class RoofStockDBContext : DbContext
    {
        private readonly IRoofStockSeedData roofStockSeedData;

        public RoofStockDBContext([NotNull] DbContextOptions options, IRoofStockSeedData roofStockSeedData) : base(options)
        {
            this.roofStockSeedData = roofStockSeedData;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries().ToList().ForEach(entity => {
                var baseEntity = (Base)entity.Entity;
                var utcNowTime = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedOn = utcNowTime;
                }
                baseEntity.ModifiedOn = utcNowTime;
            });
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().Property(p => p.GrossYieldPercentage)
                .HasComputedColumnSql("ROUND(([MonthlyRent] * 12 / [ListPrice]) * 100.00, 2)", true);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in GetStringTypeProperties(entityType))
                {
                    if (!property.GetAnnotations().Any(p => p.Name == "MaxLength"))
                    {
                        property.AddAnnotation("MaxLength", 50);
                    }
                }
            }
        }

        private static IEnumerable<IMutableProperty> GetStringTypeProperties(IMutableEntityType entityType)
        {
            return entityType.GetProperties().Where(p => p.ClrType == typeof(string));
        }
             

        public DbSet<Property> Properties { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
