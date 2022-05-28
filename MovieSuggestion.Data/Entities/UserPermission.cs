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
    [Table("user_permission")]
    public class UserPermission : BaseEntity
    {

        [Column("user_id")]
        public ulong UserId { get; set; }

        public User User { get; set; }

        [Column("permission", TypeName = "VARCHAR(32)")]
        public Permission.Values Permission { get; set; }

        internal static void FluentInitAndSeed(ModelBuilder modelBuilder, EnumToStringConverter<EntityStatus.Values> statusConverter, EnumToStringConverter<Permission.Values> permissionConverter)
        {
            FluentInit<UserPermission>(modelBuilder, statusConverter);
            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.User).WithMany().HasForeignKey(c => c.UserId).HasPrincipalKey(s => s.Id).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => new { e.Permission, e.UserId }).IsUnique();
                entity.Property(e => e.Permission).HasConversion(permissionConverter);
                entity.HasOne<Permission>().WithMany().HasForeignKey(c => c.Permission).OnDelete(DeleteBehavior.Restrict);


            });
        }
    }
}
