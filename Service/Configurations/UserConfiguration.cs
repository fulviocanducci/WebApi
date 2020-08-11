using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace Service.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id");
            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.Password)
                .HasColumnName("password")
                .HasMaxLength(100);
        }

        public static UserConfiguration Create()
        {
            return new UserConfiguration();
        }
    }
}
