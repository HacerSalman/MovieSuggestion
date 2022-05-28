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
        public Guid Username { get; set; }

        [Column("email")]
        [StringLength(36)]
        public string Email { get; set; }

        [Column("password")]
        [StringLength(512)]
        public string Password { get; set; }

        internal static void FluentInitAndSeed(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter)
        {
            FluentInit<User>(modelBuilder, statusConverter);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Email).IsUnique(true);
                entity.HasAlternateKey(e => e.Username);
            });
        }
    }
}
