using ResumeManagementApplication.Shared.Models;

namespace ResumeManagementApplication.Shared.Abstractions
{
    public interface IDegreeService
    {
        Task<List<Degree>> GetAllAsync();
        Task<List<Degree>> GetByIdsAsync(IEnumerable<int> ids);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> SaveAsync(Degree degree);
        Task<bool> RemoveUnusedDegreesAsync();
    }
}