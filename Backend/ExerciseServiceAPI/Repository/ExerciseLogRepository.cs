using ExerciseServiceAPI.Data;
using ExerciseServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseServiceAPI.Repository
{
    public class ExerciseLogRepository : IExerciseLogRepository
    {
        private readonly ExerciseDbContext _context;

        public ExerciseLogRepository(ExerciseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExerciseLog>> GetExerciseLogsAsync()
        {
            return await _context.ExerciseLogs.ToListAsync();
        }

        public async Task<ExerciseLog> GetExerciseLogByIdAsync(int logId)
        {
            return await _context.ExerciseLogs.FindAsync(logId);
        }

        public async Task InsertExerciseLogAsync(ExerciseLog exerciseLog)
        {
            await _context.ExerciseLogs.AddAsync(exerciseLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExerciseLogAsync(ExerciseLog exerciseLog)
        {
            _context.ExerciseLogs.Update(exerciseLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExerciseLogAsync(int logId)
        {
            var exerciseLog = await _context.ExerciseLogs.FindAsync(logId);
            if (exerciseLog != null)
            {
                _context.ExerciseLogs.Remove(exerciseLog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExerciseLog>> GetLogsForPast7DaysAsync(string userName)
        {
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);
            return await _context.ExerciseLogs
                .Where(log => log.Username == userName && log.ExerciseDate >= sevenDaysAgo)
                .ToListAsync();
        }

        public async Task<List<ExerciseLog>> GetExerciseLogsByUsernameAndDateAsync(string username, DateTime date)
        {
            try
            {
                // Fetch logs matching the username and the date
                var exerciseLogs = await _context.ExerciseLogs
                    .Where(el => el.Username == username && el.ExerciseDate.Date == date.Date)
                    .ToListAsync();

                return exerciseLogs;
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetExerciseLogsByUsernameAndDateAsync: {ex.Message}");
                throw new Exception("Failed to retrieve exercise logs.");
            }
        }

        public async Task<List<ExerciseLog>> GetExerciseLogsByUsernameAsync(string username)
        {
            try
            {
                // Fetch logs matching the username
                var exerciseLogs = await _context.ExerciseLogs
                    .Where(el => el.Username == username)
                    .ToListAsync();

                return exerciseLogs;
            }
            catch (Exception ex)
            {
                // Log the exception (use proper logging)
                Console.Error.WriteLine($"Error in GetExerciseLogsByUsernameAsync: {ex.Message}");
                throw new Exception("Failed to retrieve exercise logs.");
            }
        }


    }
}
