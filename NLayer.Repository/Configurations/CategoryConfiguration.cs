﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn(); //kaçar kaçar artacağını belirleyebiliriz
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

			//builder.ToTable("Category"); tablo ismini belirtebiliriz.
		}
	}
}
