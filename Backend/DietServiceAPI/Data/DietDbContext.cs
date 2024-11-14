using Microsoft.EntityFrameworkCore;
using DietServiceAPI.Models;

namespace DietServiceAPI.Data
{
    public class DietDbContext : DbContext
    {
        public DietDbContext(DbContextOptions<DietDbContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodDetails> FoodDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships using Fluent API without navigation properties

            modelBuilder.Entity<Food>()
                .HasOne<Meal>() // Meal-Food one-to-many relationship
                .WithMany()
                .HasForeignKey(f => f.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodDetails>()
                .HasOne<Food>() // Food-FoodDetails one-to-one relationship
                .WithOne()
                .HasForeignKey<FoodDetails>(fd => fd.FoodId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}