﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
	internal class CategorySeed : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			//seed data ile veri eklerken id belirtmeliyiz
			builder.HasData(new Category
			{
				Id = 1,
				Name = "Kalem"
			}, new Category
			{
				Id = 2,
				Name = "Kitap"
			}, new Category
			{
				Id = 3,
				Name = "Defter"
			});
		}
	}
}
