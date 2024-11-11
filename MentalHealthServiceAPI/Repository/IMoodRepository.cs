using MentalHealthServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentalHealthServiceAPI.Repository
{
    public interface IMoodRepository
    {
        Task<IEnumerable<Mood>> GetMoodsAsync();

        Task<Mood> GetMoodByIdAsync(int moodId);

        Task InsertMoodAsync(Mood mood);

        Task DeleteMoodAsync(int moodId);

        Task UpdateMoodAsync(Mood mood);

        Task SaveAsync();
    }
}
