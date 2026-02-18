using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace blogdatalayer.dbcontext
{
    public class appdbcontextfactory : IDesignTimeDbContextFactory<appdbcontext>
    {
        public appdbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<appdbcontext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=BlogManagement;User Id=sa;Password=123456;TrustServerCertificate=True;");

            return new appdbcontext(optionsBuilder.Options);
        }
    }
}
