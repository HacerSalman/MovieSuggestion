using Microsoft.EntityFrameworkCore;
using MovieSuggestion.Data.Entities;
using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
