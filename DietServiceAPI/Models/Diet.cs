using System.ComponentModel.DataAnnotations;

namespace DietServiceAPI.Models
{
    public class Diet
    {
        [Key]
        public int DietId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FoodItem { get; set; }
        [Required]
        public float Quantity { get; set; }
        public int Calories { get; set; }
        [Required]
        public  string MealType { get; set; }
        public DateTime ConsumptionDate { get; set; }
    }
}
