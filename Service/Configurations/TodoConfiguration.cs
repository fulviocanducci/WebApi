using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;

namespace Service.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id");
            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(p => p.Done)
                .HasColumnName("done");
        }

        public static TodoConfiguration Create()
        {
            return new TodoConfiguration();
        }
    }
}
