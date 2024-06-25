using ResumeManagementApplication.Shared.Models;

namespace ResumeManagementApplication.Shared.Abstractions
{
    public interface ICandidateService
    {
        Task<List<Candidate>> GetAllAsync();
        Task<List<Candidate>> GetByIdsAsync(IEnumerable<int> ids);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> SaveAsync(Candidate candidate);
    }
}