using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogdatalayer.configuration
{
    public class postconfig : IEntityTypeConfiguration<post>
    {
        public void Configure(EntityTypeBuilder<post> builder)
        {
            builder.ToTable("posts");
            builder.HasKey(p => p.id);

            builder.Property(p => p.id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.title).IsRequired().HasColumnType("varchar(100)");

            builder.Property(p => p.content).IsRequired().HasColumnType("varchar(300)");

            builder.Property(p => p.createdat).IsRequired();

            builder.Property(p => p.ispublished).IsRequired();

            builder.HasOne(p => p.author).WithMany(a => a.posts).HasForeignKey(p => p.authorid).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
