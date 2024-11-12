using System.ComponentModel.DataAnnotations;

namespace DietServiceAPI.Models
{
    public class Diet
    {
        [Key]
        public int DietId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string FoodItem { get; set; } = string.Empty;
        [Required]
        public float Quantity { get; set; }
        public int Calories { get; set; }
        [Required]
        public string MealType { get; set; } = string.Empty;
        public DateTime ConsumptionDate { get; set; }
    }
}
