using blogdatalayer.configuration;
using blogdatalayer.entities;
using Microsoft.EntityFrameworkCore;

namespace blogdatalayer.dbcontext
{
    public class appdbcontext : DbContext
    {
        public appdbcontext(DbContextOptions<appdbcontext> options) : base(options) { }

        public DbSet<user> users { get; set; }
        public DbSet<post> posts { get; set; }
        public DbSet<comment> comments { get; set; }
        public DbSet<like> likes { get; set; }
        public DbSet<commentlike> commentlikes { get; set; }
        public DbSet<reply> replies { get; set; }
        public DbSet<replylike> replylikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new userconfig());
            modelBuilder.ApplyConfiguration(new postconfig());
            modelBuilder.ApplyConfiguration(new commentconfig());
            modelBuilder.ApplyConfiguration(new likeconfig());
            modelBuilder.ApplyConfiguration(new commentlikeconfig());
            modelBuilder.ApplyConfiguration(new replyconfig());
            modelBuilder.ApplyConfiguration(new replylikeconfig());
        }
    }
}
