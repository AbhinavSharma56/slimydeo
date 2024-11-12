using MentalHealthServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentalHealthServiceAPI.Repository
{
    public interface IMentalHealthLogRepository
    {
        Task<IEnumerable<MentalHealthLog>> GetMentalHealthLogsAsync();

        Task<MentalHealthLog> GetMentalHealthLogByIdAsync(int mentalHealthLogId);

        Task InsertMentalHealthLogAsync(MentalHealthLog mentalHealthLog);

        Task DeleteMentalHealthLogAsync(int mentalHealthLogId);

        Task UpdateMentalHealthLogAsync(MentalHealthLog mentalHealthLog);
    }
}
