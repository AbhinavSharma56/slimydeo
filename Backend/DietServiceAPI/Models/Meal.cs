using System.ComponentModel.DataAnnotations;

namespace DietServiceAPI.Models
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string MealType { get; set; } = string.Empty;

        [Required]
        public DateTime ConsumptionDate { get; set; }
    }
}
