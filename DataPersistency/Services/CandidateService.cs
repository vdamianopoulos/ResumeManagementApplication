using DataPersistency.Abstractions;
using DataPersistency.Extentions;
using DataPersistency.Models;

namespace DataPersistency.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ILogger<CandidateService> _logger;
        private readonly IGenericRepository<Candidate> _repo;
        private readonly ICandidateSpecificOperationsRepository _candidateSpecificOperationsRepository;

        public CandidateService(
            ILogger<CandidateService> logger,
            IGenericRepository<Candidate> repo,
            ICandidateSpecificOperationsRepository candidateSpecificOperationsRepository)
        {
            _logger = logger;
            _repo = repo;
            _candidateSpecificOperationsRepository = candidateSpecificOperationsRepository;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IEnumerable<Candidate>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _repo.GetByIdsAsync(ids);
        }

        public async Task<bool> DeleteByIdAsync(int candidateId)
        {
            return await _repo.DeleteByIdAsync(candidateId);
        }

        public async Task<bool> SaveAsync(Candidate candidate)
        {
            bool success = false;

            if (candidate == null)
                return false;

            var existingCandidateList = await _repo.GetByIdsAsync(new List<int>() { candidate.Id });

            if (existingCandidateList == null)
                return success;

            if (!existingCandidateList.Any())
            {
                success = await _repo.AddAsync(candidate);
            }
            else if (existingCandidateList.Count() == 1)
            {
                var existingCandidate = existingCandidateList.First();

                _candidateSpecificOperationsRepository.AttachModelInDb(existingCandidate);
                existingCandidate.UpdateValues(candidate);
                success = await _repo.UpdateAsync(existingCandidate);
            }

            return success;
        }
    }
}
