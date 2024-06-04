using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebAPISample.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext appDbContext;
        private DbSet<T> entities;

        public Repository(DbContext context)
        {
            appDbContext = context;
            entities = appDbContext.Set<T>();
        }

        public Task SaveChangeAsync() => appDbContext.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction()
        {
            return appDbContext.Database.BeginTransaction();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            var entity = await entities.FindAsync(id);
            if (entity == null)
            {
                return null!;
            }

            return entity;
        }

        public async Task CreateAsync(T entity)
        {
            await entities.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }
    }
}