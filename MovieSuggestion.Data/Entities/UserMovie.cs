using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieSuggestion.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSuggestion.Data.Entities
{
    [Table("user_movie")]
    public class UserMovie : BaseEntity
    {
        [Column("user_id")]
        public long UserId { get; set; }

        public User User { get; set; }

        [Column("movie_id")]
        public long MovieId { get; set; }

        public Movie Movie { get; set; }

        [Column("score")]
        public double Score { get; set; }


        internal static void FluentInitAndSeed(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter)
        {
            FluentInit<UserMovie>(modelBuilder, statusConverter);
         
            modelBuilder.Entity<UserMovie>(entity =>
            {               
                entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Movie).WithMany().HasForeignKey(e => e.MovieId).HasPrincipalKey(m => m.Id).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => new { e.UserId, e.MovieId }).IsUnique();
            });
        }
    }
}
