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
    public class BaseEntity
    {
        [Column("id")]
        public ulong Id { get; set; }

        [Column("status", TypeName = "VARCHAR(32)")]
        public EntityStatus.Values Status { get; set; }

        [Column("create_time")]
        public long CreateTime { get; set; }

        [Column("Update_time")]
        public long UpdateTime { get; set; }

        [Column("owner")]
        public string Owner { get; set; }

        [Column("modifier")]
        public string Modifier { get; set; }
        internal static void FluentInit<T>(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter) where T : BaseEntity
        {
            modelBuilder.Entity<T>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Status).HasConversion(statusConverter);
                entity.HasOne<EntityStatus>().WithMany().HasForeignKey(s => s.Status).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => e.CreateTime);
                entity.HasIndex(e => e.UpdateTime);

            });
        }
    }
}
