using MentalHealthServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MentalHealthServiceAPI.Data
{
    public class MentalHealthDbContext : DbContext
        {
            public MentalHealthDbContext(DbContextOptions<MentalHealthDbContext> options) : base(options) { }

            // Define DbSets for the models
            public DbSet<MentalHealthLog> MentalHealthLogs { get; set; }
            public DbSet<Mood> Moods { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configure the MentalHealthLog entity
                modelBuilder.Entity<MentalHealthLog>(entity =>
                {
                    entity.HasKey(e => e.LogId);  // Set primary key
                    entity.Property(e => e.Username).IsRequired();
                    entity.Property(e => e.MoodId).IsRequired();
                    entity.Property(e => e.Intensity).IsRequired();
                    entity.Property(e => e.Notes).HasMaxLength(500);  // Optional length restriction
                    entity.Property(e => e.LogDate).IsRequired();

                    // Define relationships, if any
                    entity.HasOne<Mood>()
                          .WithMany()
                          .HasForeignKey(e => e.MoodId)
                          .OnDelete(DeleteBehavior.Restrict); // Ensures Mood must exist to create a log
                });

                // Configure the Mood entity
                modelBuilder.Entity<Mood>(entity =>
                {
                    entity.HasKey(e => e.MoodId);  // Set primary key
                    entity.Property(e => e.MoodName).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.Description).HasMaxLength(500);
                    entity.Property(e => e.CreatedBy).IsRequired();
                    entity.Property(e => e.CreatedAt).IsRequired();
                });

                base.OnModelCreating(modelBuilder);
            }
        }
}

