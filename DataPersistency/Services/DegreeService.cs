using DataPersistency.Abstractions;
using DataPersistency.Extentions;
using DataPersistency.Models;

namespace DataPersistency.Services
{
    public class DegreeService : IDegreeService
    {
        private ILogger<DegreeService> _logger;
        private IGenericRepository<Degree> _repo;
        private IDegreeSpecificOperationsRepository _degreeSpecificOperationsRepository;

        public DegreeService(
            ILogger<DegreeService> logger,
            IGenericRepository<Degree> repo,
            IDegreeSpecificOperationsRepository degreeSpecificOperationsRepository)
        {
            _logger = logger;
            _repo = repo;
            _degreeSpecificOperationsRepository = degreeSpecificOperationsRepository;
        }

        public async Task<IEnumerable<Degree>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IEnumerable<Degree>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _repo.GetByIdsAsync(ids);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }

        public async Task<bool> SaveAsync(Degree degree)
        {
            bool success = false;

            if (degree == null)
                return false;

            var existingDegreeList = await _repo.GetByIdsAsync(new List<int>() { degree.Id });

            if (existingDegreeList == null)
                return success;

            if (!existingDegreeList.Any())
            {
                success = await _repo.AddAsync(degree);
            }
            else if (existingDegreeList.Count() == 1)
            {
                var existingDegree = existingDegreeList.First();

                _degreeSpecificOperationsRepository.AttachModelInDb(existingDegree);
                existingDegree.UpdateValues(degree);
                success = await _repo.UpdateAsync(existingDegree);
            }

            return success;
        }

        public async Task<bool> RemoveUnusedDegreesAsync()
        {
            return await _degreeSpecificOperationsRepository.RemoveUnusedDegrees();
        }
    }
}
