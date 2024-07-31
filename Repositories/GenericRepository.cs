using LojinhaDaPaulinhaAPI.Data;
using LojinhaDaPaulinhaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojinhaDaPaulinhaAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataContext dataContext)
        {
            _dbSet = dataContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            return _dbSet.Remove(await _dbSet.FindAsync(id)).Entity;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetAsNoTrackingAsync(int id)
        {
            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<SelectListItem>> GetComboAsync()
        {
            return await _dbSet.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString(),
            })
            .ToListAsync();
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
