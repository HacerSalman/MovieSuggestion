using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Data.Entities
{
    [Table("user_movie_note")]
    public class UserMovieNote : BaseEntity
    {
        [Column("user_id")]
        public ulong UserId { get; set; }

        public User User { get; set; }

        [Column("movie_id")]
        public ulong MovieId { get; set; }

        public Movie Movie { get; set; }

        [Column("note")]
        [StringLength(2048)]
        public string Note { get; set; }

        internal static void FluentInitAndSeed(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter)
        {
            FluentInit<UserMovieNote>(modelBuilder, statusConverter);

            modelBuilder.Entity<UserMovieNote>(entity =>
            {
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Movie).WithMany().HasForeignKey(e => e.MovieId).HasPrincipalKey(m => m.Id).OnDelete(DeleteBehavior.Restrict);
               
            });
        }
    }
}
