
using System.Reflection;
using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Product> Products  {get;set;}
        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }



        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.sqLite")
            {
                 foreach (var entityType in modelBuilder.Model.GetEntityTypes())

                {
                    var Properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType
                    == typeof (decimal));

                    foreach (var property in Properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }
                
            }
            

        
     }
        }
    }
}