﻿using BookstoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreProject.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Stock).IsRequired();
        builder.Property(x => x.AuthorName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Pages).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.ToTable("Products");
        
    }
}