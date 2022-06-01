using Microsoft.EntityFrameworkCore;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
using System.Linq;

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

        public override int SaveChanges()
        {
            OnBeforeSaveChanges();
            var result = base.SaveChanges();
           
            return result;
        }

        private void OnBeforeSaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                //When score of usermovie update or create then calculate the score of the movie
                if (entry.Entity is UserMovie)
                {
                    var userMovie = (UserMovie)entry.Entity;
                    var movie = Movies.Find(userMovie.MovieId);
                    if (movie != null)
                        movie.Score = UserMovies.Where(_ => _.Status == EntityStatus.Values.ACTIVE).AverageAsync(_ => _.Score).Result;
         
                }

            }
        }
    }
}
