using LojinhaDaPaulinhaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojinhaDaPaulinhaAPI.Data
{
    public class DataUnit : IDataUnit
    {
        private readonly DataContext _context;

        public DataUnit(DataContext dataContext,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _context = dataContext;
            
            Products = productRepository;
            Users = userRepository;
        }

        public IProductRepository Products { get; }
        public IUserRepository Users { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
