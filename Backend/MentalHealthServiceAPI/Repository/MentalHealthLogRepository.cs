using MentalHealthServiceAPI.Data;
using MentalHealthServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentalHealthServiceAPI.Repository
{
    public class MentalHealthLogRepository : IMentalHealthLogRepository
    {
        private readonly MentalHealthDbContext _dbContext;

        public MentalHealthLogRepository(MentalHealthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MentalHealthLog>> GetMentalHealthLogsAsync()
        {
            return await _dbContext.MentalHealthLogs.ToListAsync();
        }

        public async Task<MentalHealthLog> GetMentalHealthLogByIdAsync(int mentalHealthLogId)
        {
            return await _dbContext.MentalHealthLogs.FindAsync(mentalHealthLogId);
        }

        public async Task InsertMentalHealthLogAsync(MentalHealthLog mentalHealthLog)
        {
            await _dbContext.MentalHealthLogs.AddAsync(mentalHealthLog);
            await SaveAsync();
        }

        public async Task UpdateMentalHealthLogAsync(MentalHealthLog mentalHealthLog)
        {
            _dbContext.Entry(mentalHealthLog).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task DeleteMentalHealthLogAsync(int mentalHealthLogId)
        {
            var mentalHealthLog = await _dbContext.MentalHealthLogs.FindAsync(mentalHealthLogId);
            if (mentalHealthLog != null)
            {
                _dbContext.MentalHealthLogs.Remove(mentalHealthLog);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<MentalHealthLog>> GetMentalHealthLogByUsernameAsync(string username)
        {
            return await _dbContext.MentalHealthLogs
                .Where(x => x.Username == username)
                .ToListAsync();
        }


        private async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
