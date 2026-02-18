using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogdatalayer.configuration
{
    public class likeconfig : IEntityTypeConfiguration<like>
    {
        public void Configure(EntityTypeBuilder<like> builder)
        {
            builder.ToTable("likes");
            builder.HasKey(l => l.id);

            builder.Property(l => l.id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(l => l.createdat).IsRequired();

            builder.HasOne(l => l.user).WithMany(u => u.likes).HasForeignKey(l => l.userid).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.post).WithMany(p => p.likes).HasForeignKey(l => l.postid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
