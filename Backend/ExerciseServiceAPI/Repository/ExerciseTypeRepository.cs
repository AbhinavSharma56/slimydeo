using ExerciseServiceAPI.Data;
using ExerciseServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Repository
{
    public class ExerciseTypeRepository : IExerciseTypeRepository
    {
        private readonly ExerciseDbContext _context;

        public ExerciseTypeRepository(ExerciseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExerciseType>> GetExerciseTypesAsync()
        {
            return await _context.ExerciseTypes.ToListAsync();
        }

        public async Task<ExerciseType> GetExerciseTypeByIdAsync(int exerciseTypeId)
        {
            return await _context.ExerciseTypes.FindAsync(exerciseTypeId);
        }

        public async Task InsertExerciseTypeAsync(ExerciseType exerciseType)
        {
            await _context.ExerciseTypes.AddAsync(exerciseType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExerciseTypeAsync(ExerciseType exerciseType)
        {
            _context.ExerciseTypes.Update(exerciseType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExerciseTypeAsync(int exerciseTypeId)
        {
            var exerciseType = await _context.ExerciseTypes.FindAsync(exerciseTypeId);
            if (exerciseType != null)
            {
                _context.ExerciseTypes.Remove(exerciseType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
