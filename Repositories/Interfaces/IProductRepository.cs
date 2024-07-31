using LojinhaDaPaulinhaAPI.Dtos.Product;
using LojinhaDaPaulinhaAPI.Entities;

namespace LojinhaDaPaulinhaAPI.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> CreateFromObjectAsync(object value);
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<IndexRowProductDto>> GetAllAsync();
        Task<DisplayProductDto> GetDisplayModelAsync(int id);
        Task<EditProductDto> GetEditModelAsync(int id);
        Task<int> GetIdFromNameAsync(string name);
        Product UpdateFromObject(object value);
    }
}
