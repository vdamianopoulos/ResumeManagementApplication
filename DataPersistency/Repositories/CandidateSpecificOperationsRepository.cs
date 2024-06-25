using DataPersistency.Abstractions;
using DataPersistency.DataContext;
using DataPersistency.Models;

namespace DataPersistency.Repositories
{
    public class CandidateSpecificOperationsRepository : ICandidateSpecificOperationsRepository
    {
        private readonly ILogger<CandidateSpecificOperationsRepository> _logger;
        private readonly ResumeContext _db;

        public CandidateSpecificOperationsRepository(ILogger<CandidateSpecificOperationsRepository> logger, ResumeContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void AttachModelInDb(Candidate candidate)
        {
            _db.Candidates.Attach(candidate);
        }
    }
}
