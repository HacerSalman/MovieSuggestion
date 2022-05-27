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
    [Table("user")]
    public class User : BaseEntity
    {
        [Column("name")]
        [StringLength(512)]
        [Required]
        public string Name { get; set; }

        [Column("username")]
        [StringLength(36)]
        public Guid Username { set { value = Guid.NewGuid(); } }

        [Column("email")]
        [StringLength(36)]
        public string Email { get; set; }

        [Column("password")]
        [StringLength(512)]
        public string Password { get; set; }

        internal static void FluentInitAndSeed(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter)
        {
            FluentInit<Movie>(modelBuilder, statusConverter);
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Score);
            });
        }
    }
}
