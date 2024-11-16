namespace DietServiceAPI.Models
{
    public class FoodDetailsRequestDTO
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}