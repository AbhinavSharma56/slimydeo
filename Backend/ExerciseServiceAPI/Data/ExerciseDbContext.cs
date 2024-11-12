using ExerciseServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExerciseServiceAPI.Data
{
    public class ExerciseDbContext : DbContext
    {
        public ExerciseDbContext(DbContextOptions<ExerciseDbContext> options) : base(options) { }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseLog>(entity =>
            {
                entity.HasKey(e => e.LogId);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.ExerciseTypeId).IsRequired();
                entity.Property(e => e.Duration).IsRequired();
                entity.Property(e => e.CaloriesBurned).IsRequired();
                entity.Property(e => e.ExerciseDate).IsRequired();
            });

            modelBuilder.Entity<ExerciseType>(entity =>
            {
                entity.HasKey(e => e.ExerciseTypeId);
                entity.Property(e => e.ExerciseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedBy).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
