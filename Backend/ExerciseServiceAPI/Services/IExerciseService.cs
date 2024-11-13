namespace ExerciseServiceAPI.Services
{
    public interface IExerciseService
    {
        Task<int?> CalculateCaloriesBurnedAsync(string exerciseName, TimeSpan duration);
        Task<(bool success, string message, object? data)> GetTotalCaloriesBurnedPerDayAsync(string userName);
    }
}
