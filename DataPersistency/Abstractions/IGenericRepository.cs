namespace DataPersistency.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
    }
}