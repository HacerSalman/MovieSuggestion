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
    [Table("movie")]
    public class Movie : BaseEntity
    {
        [Column("name")]
        [StringLength(512)]
        [Required]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(2048)]
        public string Description { get; set; }

        [Column("score")]
        public double Score { get; set; }


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
