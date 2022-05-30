using Microsoft.EntityFrameworkCore;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;

namespace MovieSuggestion.Data.Contexts
{
    public class MovieDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<UserMovieNote> UserMovieNotes { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(9000);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var statusConverter = EntityStatus.FluentInitAndSeed(modelBuilder);
            var permissionConverter = Permission.FluentInitAndSeed(modelBuilder);

            Movie.FluentInitAndSeed(modelBuilder, statusConverter);
            User.FluentInitAndSeed(modelBuilder, statusConverter);
            UserMovie.FluentInitAndSeed(modelBuilder, statusConverter);
            UserMovieNote.FluentInitAndSeed(modelBuilder, statusConverter);
            UserPermission.FluentInitAndSeed(modelBuilder, statusConverter, permissionConverter);
        }
    }
}
