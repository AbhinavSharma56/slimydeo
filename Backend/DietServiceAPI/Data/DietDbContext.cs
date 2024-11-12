using DietServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DietServiceAPI.Data
{
    public class DietDbContext : DbContext
    {
        public DietDbContext(DbContextOptions<DietDbContext> options) : base(options) { }

        public DbSet<Diet> Diets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diet>(entity =>
            {
                entity.HasKey(e => e.DietId);  // Set primary key
                entity.Property(e => e.UserId).IsRequired();  // User associated with the diet entry
                entity.Property(e => e.FoodItem).IsRequired().HasMaxLength(100);  // Name of food item
                entity.Property(e => e.Quantity).IsRequired();  // Quantity of food consumed
                entity.Property(e => e.Calories).IsRequired();  // Calories in the food item
                entity.Property(e => e.MealType).IsRequired().HasMaxLength(50);  // Type of meal (breakfast, lunch, dinner)
                entity.Property(e => e.ConsumptionDate).IsRequired();  // Date of consumption

                // Define relationships if needed (e.g., User or other entities)
                // You can add any additional relationship logic here
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
