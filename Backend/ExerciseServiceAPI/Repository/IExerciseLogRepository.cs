using ExerciseServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Repository
{
    public interface IExerciseLogRepository
    {
        Task<IEnumerable<ExerciseLog>> GetExerciseLogsAsync();
        Task<ExerciseLog> GetExerciseLogByIdAsync(int logId);
        Task InsertExerciseLogAsync(ExerciseLog exerciseLog);
        Task UpdateExerciseLogAsync(ExerciseLog exerciseLog);
        Task DeleteExerciseLogAsync(int logId);
    }
}