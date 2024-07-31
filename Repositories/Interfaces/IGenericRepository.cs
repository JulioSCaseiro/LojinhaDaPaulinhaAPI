using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojinhaDaPaulinhaAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<T>> GetAllAsNoTrackingAsync();
        Task<T> GetAsNoTrackingAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboAsync();
        T Update(T entity);
    }
}
