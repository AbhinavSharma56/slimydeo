using ExerciseServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Repository
{
    public interface IExerciseTypeRepository
    {
        Task<IEnumerable<ExerciseType>> GetExerciseTypesAsync();
        Task<ExerciseType> GetExerciseTypeByIdAsync(int exerciseTypeId);
        Task InsertExerciseTypeAsync(ExerciseType exerciseType);
        Task UpdateExerciseTypeAsync(ExerciseType exerciseType);
        Task DeleteExerciseTypeAsync(int exerciseTypeId);
    }
}
