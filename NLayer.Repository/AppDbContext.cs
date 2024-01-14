using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
	public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
            //Product üzerinden verilen  değerlere göre ProductFeature oluşturmak için kullanırız.
            //var product = new Product(){ ProductFeature = new ProductFeature() { }}
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            //her class library bir assemblydir 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //çalıştığımız yeri alır.
            //modelBuilder.ApplyConfiguration(new CategoryConfiguration()); tek tek verebiliriz

            //farklı kullanım seed klasörü dışında da yapabiliriz.
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1
            }, new ProductFeature
			{
				Id = 2,
				Color = "Sarı",
				Height = 300,
				Width = 300,
				ProductId = 2
			});
			base.OnModelCreating(modelBuilder);
		}
	}
}
