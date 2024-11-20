using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTimePredicter.Models;

namespace TaskTimePredicter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(
            DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Quest> Quests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tabla "Users"
            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFF1714BBE7");
                    entity.HasIndex(e => e.UserEmail, "UQ__Users__D54ADF555E66784B").IsUnique();
                    entity.Property(e => e.UserId).HasColumnName("userId");
                    entity.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("(getdate())")
                        .HasColumnName("createdAt");
                    entity.Property(e => e.UserEmail)
                        .HasMaxLength(100)
                        .HasColumnName("userEmail");
                    entity.Property(e => e.UserName)
                        .HasMaxLength(100)
                        .HasColumnName("userName");
                    entity.Property(e => e.UserPassword)
                        .HasMaxLength(100)
                        .HasColumnName("userPassword");
                    entity.Property(e => e.UserRole)
                        .HasMaxLength(50)
                        .HasDefaultValue("Developer")
                        .HasColumnName("userRole");
                    entity.HasData(new User
                    {
                        UserId = 001,
                        UserName = "Administrador Base",
                        UserPassword = "admin123",
                        UserRole = "Administrator",
                        UserEmail = "admin@gmail.com",
                        CreatedAt = DateOnly.FromDateTime(DateTime.Now)
                    });
                });

            //Tabla "Categories"
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Categori__23CAF1D81A434EC4");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");
                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(255)
                    .HasColumnName("categoryDescription");
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("categoryName");
            });

            //Tabla "Quests"
            modelBuilder.Entity<Quest>(entity =>
            {
                entity.HasKey(e => e.QuestId);

                entity.Property(e => e.QuestId).HasColumnName("taskId");
                entity.Property(e => e.ActualTime).HasColumnName("actualTime");
                entity.Property(e => e.CategoryId).HasColumnName("categoryId");
                entity.Property(e => e.CreationDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnName("creationDate");
                entity.Property(e => e.EstimatedTime).HasColumnName("estimatedTime");
                entity.Property(e => e.QuestName)
                    .HasMaxLength(100)
                    .HasColumnName("questName");
                entity.Property(e => e.QuestState)
                    .HasMaxLength(50)
                    .HasDefaultValue("In Progress")
                    .HasColumnName("questState");
                entity.Property(e =>  e.UserId).HasColumnName("userId");
                //Relaciones FK
                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
