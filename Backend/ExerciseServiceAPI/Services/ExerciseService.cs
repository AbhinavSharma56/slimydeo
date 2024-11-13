
using ExerciseServiceAPI.Repository;
using Newtonsoft.Json.Linq;

namespace ExerciseServiceAPI.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IExerciseLogRepository _exerciseLogRepository;

        public ExerciseService(IHttpClientFactory httpClientFactory, IExerciseLogRepository exerciseLogRepository)
        {
            _httpClientFactory = httpClientFactory;
            _exerciseLogRepository = exerciseLogRepository;
        }

        public async Task<int?> CalculateCaloriesBurnedAsync(string exerciseName, TimeSpan duration)
        {
            var client = _httpClientFactory.CreateClient("Nutritionix");
            string query = $"{exerciseName} for {duration.TotalMinutes} minutes";
            var requestBody = new { query };
            var response = await client.PostAsJsonAsync("natural/exercise", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
                return jsonResponse["exercises"]?[0]?["nf_calories"]?.Value<int>();
            }

            return null;
        }

        public async Task<(bool success, string message, object? data)> GetTotalCaloriesBurnedPerDayAsync(string userName)
        {
            try
            {
                var logs = await _exerciseLogRepository.GetLogsForPast7DaysAsync(userName);

                if (logs == null || !logs.Any())
                {
                    return (false, "No exercise logs found for this user in the past 7 days.", null);
                }

                var caloriesPerDay = logs
                    .GroupBy(log => log.ExerciseDate.Date)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Sum(log => log.CaloriesBurned)
                    );

                return (true, "Total calories burned successfully calculated.", caloriesPerDay);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}", null);
            }
        }
    }
}