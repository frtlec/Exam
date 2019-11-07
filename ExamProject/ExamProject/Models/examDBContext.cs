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

        public virtual DbSet<ExamQuestions> ExamQuestions { get; set; }
        public virtual DbSet<Exams> Exams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlite("DataSource=C:\\db\\examDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ExamQuestions>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.Property(e => e.QuestionId).ValueGeneratedOnAdd();

                entity.Property(e => e.Answer).IsRequired();

                entity.Property(e => e.OptionA).IsRequired();

                entity.Property(e => e.OptionB).IsRequired();

                entity.Property(e => e.OptionC).IsRequired();

                entity.Property(e => e.OptionD).IsRequired();
            });

            modelBuilder.Entity<Exams>(entity =>
            {
                entity.HasKey(e => e.ExamId);

                entity.Property(e => e.ExamId).ValueGeneratedOnAdd();

                entity.Property(e => e.Header).IsRequired();

                entity.Property(e => e.Paragraph).IsRequired();

                entity.Property(e => e.UniqueId).IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserId)
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserPassword).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });
        }
    }
}
