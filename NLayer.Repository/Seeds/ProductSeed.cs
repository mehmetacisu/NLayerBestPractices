using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasData(new Product
			{
				Id = 1,
				CategoryId = 1,
				Name = "Pilot Kalem",
				Price = 100,
				Stock = 20,
				CreatedDate = DateTime.Now,
			},
			new Product
			{
				Id = 2,
				CategoryId = 1,
				Name = "Kurşun Kalem",
				Price = 200,
				Stock = 30,
				CreatedDate = DateTime.Now,
			},
			new Product
			{
				Id = 3,
				CategoryId = 1,
				Name = "Tahta Kalemi",
				Price = 600,
				Stock = 60,
				CreatedDate = DateTime.Now,
			},
			new Product
			{
				Id = 4,
				CategoryId = 2,
				Name = "Matematik Soru Bankası",
				Price = 250,
				Stock = 60,
				CreatedDate = DateTime.Now,
			},
			new Product
				{
				Id = 5,
				CategoryId = 2,
				Name = "Türkçe Soru Bankası",
				Price = 200,
				Stock = 60,
				CreatedDate = DateTime.Now,
			});
		}
	}
}
