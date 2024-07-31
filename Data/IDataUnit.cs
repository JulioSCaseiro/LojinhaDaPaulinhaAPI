using LojinhaDaPaulinhaAPI.Repositories.Interfaces;

namespace LojinhaDaPaulinhaAPI.Data
{
    public interface IDataUnit
    {
        IProductRepository Products { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
