using System.ComponentModel.DataAnnotations;

public class Food
{
    [Key]
    public int FoodId { get; set; }

    // Foreign Key to Meal
    public int MealId { get; set; }

    [Required]
    public string FoodName { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public string Unit { get; set; } = string.Empty;
}