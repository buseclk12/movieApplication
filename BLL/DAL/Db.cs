using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<Directors> Directors { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Movies>()
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId);

            modelBuilder.Entity<MovieGenres>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenres>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}