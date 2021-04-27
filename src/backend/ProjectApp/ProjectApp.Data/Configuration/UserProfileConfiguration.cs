using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectApp.Model.Entities.User;

namespace ProjectApp.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder
                .HasKey(m => m.UserProfileId);

            builder
                .Property(m => m.UserProfileId)
                .ValueGeneratedNever();
            // .UseIdentityColumn();

            builder
                .Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .ToTable(name: "USER_PROFILE", schema: "dbo")
                .HasDiscriminator<int>("UserType")
                .HasValue<InternalUser>(1)
                .HasValue<Client>(2);
        }
    }
}