using MentalHealthServiceAPI.Data;
using MentalHealthServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentalHealthServiceAPI.Repository
{
    public class MoodRepository : IMoodRepository
    {
        private readonly MentalHealthDbContext _dbContext;

        public MoodRepository(MentalHealthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Mood>> GetMoodsAsync()
        {
            return await _dbContext.Moods.ToListAsync();
        }

        public async Task<Mood> GetMoodByIdAsync(int moodId)
        {
            return await _dbContext.Moods.FindAsync(moodId);
        }

        public async Task InsertMoodAsync(Mood mood)
        {
            await _dbContext.Moods.AddAsync(mood);
            await SaveAsync();
        }

        public async Task UpdateMoodAsync(Mood mood)
        {
            var existingMood = await _dbContext.Moods
                .AsNoTracking() // Ensures the entity is not tracked.
                .FirstOrDefaultAsync(m => m.MoodId == mood.MoodId);

            if (existingMood != null)
            {
                // Detach the existing tracked entity if needed
                var trackedMood = _dbContext.Moods.Local.FirstOrDefault(m => m.MoodId == mood.MoodId);
                if (trackedMood != null)
                {
                    _dbContext.Entry(trackedMood).State = EntityState.Detached;
                }

                // Attach the updated entity
                _dbContext.Moods.Update(mood);

                // Save changes
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Handle case where the Mood with the given ID does not exist
                throw new Exception("Mood not found.");
            }
        }



        public async Task DeleteMoodAsync(int moodId)
        {
            var mood = await _dbContext.Moods.FindAsync(moodId);
            if (mood != null)
            {
                _dbContext.Moods.Remove(mood);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
