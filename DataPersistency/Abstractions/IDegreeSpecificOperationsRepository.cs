using DataPersistency.Models;

namespace DataPersistency.Abstractions
{
    public interface IDegreeSpecificOperationsRepository
    {
        Task<bool> RemoveUnusedDegrees();
        void AttachModelInDb(Degree degree);
    }
}