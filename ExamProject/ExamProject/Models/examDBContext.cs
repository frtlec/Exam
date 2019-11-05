using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamProject.Models
{
    public partial class examDBContext : DbContext
    {
        public examDBContext()
        {
        }

        public examDBContext(DbContextOptions<examDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseSqlite("DataSource=C:\\examDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserId)
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserPassword).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });
        }
    }
}
