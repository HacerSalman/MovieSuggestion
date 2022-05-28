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
        [Column("title")]
        [StringLength(512)]
        [Required]
        public string Title { get; set; }

        [Column("original_title")]
        [StringLength(512)]
        [Required]
        public string OriginalTitle { get; set; }

        [Column("overview")]
        [StringLength(2048)]
        public string Overview { get; set; }

        [Column("score")]
        public double Score { get; set; }

        [Column("adult")]
        public bool Adult { get; set; }

        [Column("backdrop_path")]
        [StringLength(256)]
        public string BackdropPath { get; set; }

        [NotMapped]
        public string BackdropPathUrl =>
        string.IsNullOrEmpty(BackdropPath)
            ? null
            : "https://" + Environment.GetEnvironmentVariable("MOVIE_PUBLIC_CDN") + "/" + BackdropPath;

        [Column("source_id")]
        public long SourceId { get; set; }

        [Column("original_language")]
        [StringLength(8)]
        public string OriginalLanguage { get; set; }

        [Column("popularity")]
        public double Popularity { get; set; }

        [Column("poster_path")]
        [StringLength(256)]
        public string PosterPath { get; set; }

        [NotMapped]
        public string PosterPathUrl =>
        string.IsNullOrEmpty(PosterPath)
            ? null
            : "https://" + Environment.GetEnvironmentVariable("MOVIE_PUBLIC_CDN") + "/" + PosterPath;

        [Column("video")]
        public bool Video { get; set; }
        internal static void FluentInitAndSeed(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter)
        {
            FluentInit<Movie>(modelBuilder, statusConverter);
            modelBuilder.Entity<Movie>(entity =>
            {              
                entity.HasIndex(e => e.Title);
                entity.HasIndex(e => e.Score);
            });
        }
    }
}
