using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BLL.DAL
{
    public class DbContextFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MOVIESDB;Username=buse;Password=123");

            return new Db(optionsBuilder.Options);
        }
    }
}