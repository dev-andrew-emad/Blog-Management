using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogdatalayer.configuration
{
    public class commentconfig : IEntityTypeConfiguration<comment>
    {
        public void Configure(EntityTypeBuilder<comment> builder)
        {
            builder.ToTable("comments");
            builder.HasKey(c => c.id);

            builder.Property(c => c.id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(c => c.content).IsRequired().HasColumnType("varchar(300)");

            builder.Property(c => c.createdat).IsRequired();

            builder.HasOne(c => c.user).WithMany(u => u.comments).HasForeignKey(c => c.userid).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.post).WithMany(p => p.comments).HasForeignKey(c => c.postid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
