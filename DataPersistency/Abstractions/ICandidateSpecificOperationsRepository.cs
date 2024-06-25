using DataPersistency.Models;

namespace DataPersistency.Abstractions
{
    public interface ICandidateSpecificOperationsRepository
    {
        void AttachModelInDb(Candidate candidate);
    }
}