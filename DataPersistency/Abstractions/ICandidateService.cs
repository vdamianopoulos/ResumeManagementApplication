using DataPersistency.Models;

namespace DataPersistency.Abstractions
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<IEnumerable<Candidate>> GetByIdsAsync(IEnumerable<int> ids);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> SaveAsync(Candidate id);
    }
}