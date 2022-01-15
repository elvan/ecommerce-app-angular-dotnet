using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            _ = builder.Property(product => product.Id).IsRequired();
            _ = builder.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(100);
            _ = builder.Property(product => product.Description)
                .IsRequired()
                .HasMaxLength(180);
            _ = builder.Property(product => product.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            _ = builder.Property(product => product.PictureUrl).IsRequired();
            _ = builder.HasOne(product => product.ProductBrand)
                .WithMany()
                .HasForeignKey(product => product.ProductBrandId);
            _ = builder.HasOne(product => product.ProductType)
                .WithMany()
                .HasForeignKey(product => product.ProductTypeId);
        }
    }
}
