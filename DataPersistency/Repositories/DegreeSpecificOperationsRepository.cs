using DataPersistency.Abstractions;
using DataPersistency.DataContext;
using DataPersistency.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPersistency.Repositories
{
    public class DegreeSpecificOperationsRepository : IDegreeSpecificOperationsRepository
    {
        private readonly ILogger<DegreeSpecificOperationsRepository> _logger;
        private readonly ResumeContext _db;

        public DegreeSpecificOperationsRepository(ILogger<DegreeSpecificOperationsRepository> logger, ResumeContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<bool> RemoveUnusedDegrees()
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var unusedDegrees = await _db.Degrees.Where(x => !_db.Candidates.Any(c => c.DegreeId == x.Id)).ToListAsync();

                _db.Degrees.RemoveRange(unusedDegrees);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }
        public void AttachModelInDb(Degree degree)
        {
            _db.Degrees.Attach(degree);
        }
    }
}
