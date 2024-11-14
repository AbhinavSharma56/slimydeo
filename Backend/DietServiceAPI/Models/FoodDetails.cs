using System.ComponentModel.DataAnnotations;

namespace DietServiceAPI.Models
{
    public class FoodDetails
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key to Food
        public int FoodId { get; set; }

        public float Calories { get; set; } // Calories in kilocalories
        public float TotalFat { get; set; } // Grams of total fat
        public float SaturatedFat { get; set; } // Grams of saturated fat
        public float Cholesterol { get; set; } // Milligrams of cholesterol
        public float Sodium { get; set; } // Milligrams of sodium
        public float TotalCarbohydrate { get; set; } // Grams of carbohydrates
        public float DietaryFiber { get; set; } // Grams of dietary fiber
        public float Sugars { get; set; } // Grams of sugars
        public float Protein { get; set; } // Grams of protein
        public float Potassium { get; set; } // Milligrams of potassium
        public float Phosphorus { get; set; } // Milligrams of phosphorus
    }
}
