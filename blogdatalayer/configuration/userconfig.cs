using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogdatalayer.configuration
{
    public class userconfig : IEntityTypeConfiguration<user>
    {
        public void Configure(EntityTypeBuilder<user> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.id);

            builder.Property(u => u.id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(u => u.username).IsRequired().HasColumnType("varchar(50)");

            builder.Property(u => u.password).IsRequired().HasColumnType("varchar(255)");

            builder.Property(u => u.role).IsRequired().HasColumnType("varchar(50)");
        }
    }
}
