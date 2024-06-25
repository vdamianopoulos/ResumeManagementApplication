using DataPersistency.Models;

namespace DataPersistency.Abstractions
{
    public interface IDegreeService
    {
        Task<IEnumerable<Degree>> GetAllAsync();
        Task<IEnumerable<Degree>> GetByIdsAsync(IEnumerable<int> ids);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> SaveAsync(Degree id);
        Task<bool> RemoveUnusedDegreesAsync();
    }
}