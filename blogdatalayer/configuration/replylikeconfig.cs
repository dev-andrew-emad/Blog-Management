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
    public class replylikeconfig : IEntityTypeConfiguration<replylike>
    {
        public void Configure(EntityTypeBuilder<replylike> builder)
        {
            builder.ToTable("replylikes");
            builder.HasKey(r => r.id);

            builder.Property(r => r.id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(r => r.user).WithMany(u => u.replylikes).HasForeignKey(r => r.userid).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.reply).WithMany(r => r.replylikes).HasForeignKey(r => r.replyid).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
