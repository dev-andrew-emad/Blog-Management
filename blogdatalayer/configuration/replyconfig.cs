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
    public class replyconfig : IEntityTypeConfiguration<reply>
    {
        public void Configure(EntityTypeBuilder<reply> builder)
        {
            builder.ToTable("replies");
            builder.HasKey(r => r.id);

            builder.Property(r => r.id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(r => r.content).IsRequired().HasColumnType("varchar(300)");

            builder.HasOne(r => r.user).WithMany(u => u.replies).HasForeignKey(r => r.userid).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.comment).WithMany(c => c.replies).HasForeignKey(r => r.commentid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
