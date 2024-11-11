using DietServiceAPI.Models;

namespace DietServiceAPI.Repository
{
    public interface IDietRepository
    {
        Task<IEnumerable<Diet>> GetDietsAsync();

        Task<Diet> GetDietByIdAsync(int dietId);

        Task InsertDietAsync(Diet diet);

        Task DeleteDietAsync(int dietId);

        Task UpdateDietAsync(Diet diet);
    }
}
