using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Data.Configurations
{
    public class TextConfiguration : IEntityTypeConfiguration<Text>
    {
        public void Configure(EntityTypeBuilder<Text> builder)
        {
            builder
                .HasKey(m => m.TextId);

            builder
                .Property(m => m.TextId)
                .UseIdentityColumn();

            builder
                .Property(m => m.TextContent)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(m => m.TextLength)
                .IsRequired();

            builder
                .ToTable(name: "TEXT", schema: "dbo");
        }
    }
}