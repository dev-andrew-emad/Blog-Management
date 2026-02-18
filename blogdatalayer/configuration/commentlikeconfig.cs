using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blogdatalayer.configuration
{
    public class commentlikeconfig : IEntityTypeConfiguration<commentlike>
    {
        public void Configure(EntityTypeBuilder<commentlike> builder)
        {
            builder.ToTable("commentlikes");
            builder.HasKey(c => c.id);

            builder.Property(c => c.id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(c => c.user).WithMany(u => u.commentlikes).HasForeignKey(c => c.userid).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c=>c.comment).WithMany(c=>c.commentlikes).HasForeignKey(c=>c.commentid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
