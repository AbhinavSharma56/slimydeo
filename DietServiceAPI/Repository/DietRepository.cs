using DietServiceAPI.Data;
using DietServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DietServiceAPI.Repository
    {
        public class DietRepository : IDietRepository
        {
            private readonly DietDbContext _dbContext;

            public DietRepository(DietDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            // Fetches all Diets
            public async Task<IEnumerable<Diet>> GetDietsAsync()
            {
                return await _dbContext.Diets.ToListAsync();
            }

            // Fetches a specific Diet by its ID
            public async Task<Diet> GetDietByIdAsync(int dietId)
            {
                return await _dbContext.Diets.FirstOrDefaultAsync(d => d.DietId == dietId);
            }

            // Inserts a new Diet
            public async Task InsertDietAsync(Diet diet)
            {
                await _dbContext.Diets.AddAsync(diet);
                await _dbContext.SaveChangesAsync();
            }

            // Deletes an existing Diet by its ID
            public async Task DeleteDietAsync(int dietId)
            {
                var diet = await _dbContext.Diets
                    .FirstOrDefaultAsync(d => d.DietId == dietId);

                if (diet != null)
                {
                    _dbContext.Diets.Remove(diet);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Updates an existing Diet
            public async Task UpdateDietAsync(Diet diet)
            {
                var existingDiet = await _dbContext.Diets
                    .FirstOrDefaultAsync(d => d.DietId == diet.DietId);

                if (existingDiet != null)
                {
                    _dbContext.Diets.Update(diet);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
}
