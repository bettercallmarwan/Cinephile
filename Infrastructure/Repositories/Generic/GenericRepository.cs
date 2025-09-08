using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> GetAsync(int id, params Expression<Func<TEntity, object>>[]? includes)
        {
            if(includes is not null)
            {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();

                foreach (var include in includes)
                    query = query.Include(include);

                return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            }

            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[]? includes)
        {
            if (includes is not null)
            {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();

                foreach (var include in includes)
                    query = query.Include(include);

                return await query.ToListAsync();
            }

            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();

        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();

        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
