using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BikeStoreDbContext Context;
        protected readonly DbSet<T> DbSet;

        public GenericRepository(BikeStoreDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await DbSet.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await DbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);

        public void Update(T entity) => DbSet.Update(entity);

        public void Remove(T entity) => DbSet.Remove(entity);
    }
}
