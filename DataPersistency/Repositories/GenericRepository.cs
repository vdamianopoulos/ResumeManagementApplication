using DataPersistency.Abstractions;
using DataPersistency.DataContext;
using DataPersistency.Models;
using Microsoft.EntityFrameworkCore;

namespace DataPersistency.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> track = null;
        private readonly ILogger<GenericRepository<TEntity>> _logger;
        private readonly ResumeContext _db;

        public GenericRepository(ILogger<GenericRepository<TEntity>> logger, ResumeContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var results = new List<TEntity>();
            try
            {
                results = await _db.Set<TEntity>().ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return results;
            }
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var results = new List<TEntity>();
            try
            {
                var entities = await _db.Set<TEntity>().Where(x => ids.Contains(x.Id))?.ToListAsync();
                return entities ?? results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return results;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var entity = await _db.Set<TEntity>().FindAsync(id);
                if (entity == null)
                    return true;

                track = _db.Set<TEntity>().Attach(entity);
                track.State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                track.State = EntityState.Detached;
                await _db.SaveChangesAsync();
            }

            await transaction.RollbackAsync();
            return false;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                track = _db.Set<TEntity>().Attach(entity);
                track.State = EntityState.Added;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                track.State = EntityState.Detached;
                await _db.SaveChangesAsync();
            }

            await transaction.RollbackAsync();
            return false;

        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                track = _db.Set<TEntity>().Attach(entity);
                track.State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                track.State = EntityState.Detached;
                await _db.SaveChangesAsync();
            }

            await transaction.RollbackAsync();
            return false;

        }
    }
}
