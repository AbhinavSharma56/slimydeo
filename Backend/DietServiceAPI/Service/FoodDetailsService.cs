using DietServiceAPI.Models;
using DietServiceAPI.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace DietServiceAPI.Service
{
    public class FoodDetailsService : IFoodDetailsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFoodDetailsRepository _foodDetailsRepository;

        public FoodDetailsService(IHttpClientFactory httpClientFactory, IFoodDetailsRepository foodDetailsRepository)
        {
            _httpClientFactory = httpClientFactory;
            _foodDetailsRepository = foodDetailsRepository;
        }

        public async Task<bool> AddFoodDetails(int foodId, string foodName, int quantity, string unit)
        {
            var client = _httpClientFactory.CreateClient("NutritionixDiet");

            string query = $"{quantity} {unit} {foodName}";
            var requestBody = new { query };

            var response = await client.PostAsJsonAsync("natural/nutrients", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
                var foodDetails = new FoodDetails
                {
                    FoodId = foodId,
                    Calories = jsonResponse["foods"]?[0]?["nf_calories"]?.Value<float>() ?? 0f,
                    TotalFat = jsonResponse["foods"]?[0]?["nf_total_fat"]?.Value<float>() ?? 0f,
                    SaturatedFat = jsonResponse["foods"]?[0]?["nf_saturated_fat"]?.Value<float>() ?? 0f,
                    Cholesterol = jsonResponse["foods"]?[0]?["nf_cholesterol"]?.Value<float>() ?? 0f,
                    Sodium = jsonResponse["foods"]?[0]?["nf_sodium"]?.Value<float>() ?? 0f,
                    TotalCarbohydrate = jsonResponse["foods"]?[0]?["nf_total_carbohydrate"]?.Value<float>() ?? 0f,
                    DietaryFiber = jsonResponse["foods"]?[0]?["nf_dietary_fiber"]?.Value<float>() ?? 0f,
                    Sugars = jsonResponse["foods"]?[0]?["nf_sugars"]?.Value<float>() ?? 0f,
                    Protein = jsonResponse["foods"]?[0]?["nf_protein"]?.Value<float>() ?? 0f,
                    Potassium = jsonResponse["foods"]?[0]?["nf_potassium"]?.Value<float>() ?? 0f,
                    Phosphorus = jsonResponse["foods"]?[0]?["nf_p"]?.Value<float>() ?? 0f,
                };
                await _foodDetailsRepository.AddFoodDetailsAsync(foodDetails);
                return true;
            }
            return false;  
        }
    }
}