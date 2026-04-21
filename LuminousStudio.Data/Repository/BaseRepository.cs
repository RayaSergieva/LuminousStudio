namespace LuminousStudio.Data.Repository
{
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    using Interfaces;

    public abstract class BaseRepository<TEntity, TKey>
        : IRepository<TEntity, TKey>, IAsyncRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        // Sync methods
        public TEntity? GetById(TKey id)
        {
            return this.DbSet
                .Find(id);
        }

        public TEntity? SingleOrDefault(Func<TEntity, bool> predicate)
        {
            return this.DbSet
                .SingleOrDefault(predicate);
        }

        public TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return this.DbSet
                .FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.DbSet
                .ToArray();
        }

        public int Count()
        {
            return this.DbSet
                .Count();
        }

        public IQueryable<TEntity> GetAllAttached()
        {
            return this.DbSet
                .AsQueryable();
        }

        public void Add(TEntity item)
        {
            this.DbSet.Add(item);
            this.DbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> items)
        {
            this.DbSet.AddRange(items);
            this.DbContext.SaveChanges();
        }

        public bool Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            int updateCnt = this.DbContext.SaveChanges();

            return updateCnt > 0;
        }

        public bool Update(TEntity item)
        {
            try
            {
                this.DbSet.Attach(item);
                this.DbSet.Entry(item).State = EntityState.Modified;
                this.DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }

        // Async methods
        public ValueTask<TEntity?> GetByIdAsync(TKey id)
        {
            return this.DbSet
                .FindAsync(id);
        }

        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet
                .SingleOrDefaultAsync(predicate);
        }

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            TEntity[] entities = await this.DbSet
                .ToArrayAsync();

            return entities;
        }

        public Task<int> CountAsync()
        {
            return this.DbSet
                .CountAsync();
        }

        public async Task AddAsync(TEntity item)
        {
            await this.DbSet.AddAsync(item);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
        {
            await this.DbSet.AddRangeAsync(items);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            this.DbSet.Remove(entity);
            int updateCnt = await this.DbContext.SaveChangesAsync();

            return updateCnt > 0;
        }

        public async Task<bool> UpdateAsync(TEntity item)
        {
            try
            {
                this.DbSet.Attach(item);
                this.DbSet.Entry(item).State = EntityState.Modified;
                await this.DbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SaveChangesAsync()
        {
            await this.DbContext.SaveChangesAsync();
        }
    }
}