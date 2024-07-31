using LojinhaDaPaulinhaAPI.Data;
using LojinhaDaPaulinhaAPI.Dtos.Product;
using LojinhaDaPaulinhaAPI.Entities;
using LojinhaDaPaulinhaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojinhaDaPaulinhaAPI.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _productDbSet;
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            _productDbSet = dataContext.Products;
        }

        public async Task<Product> CreateFromObjectAsync(object value)
        {
            if (value is Product product) return await CreateAsync(product);

            if (value is CreateProductDto createDto)
            {
                return await CreateAsync(new Product
                {
                    Name = createDto.Name,
                    Description = createDto.Description,
                    Category = createDto.Category,
                    Price = createDto.Price,
                    IsAvaliable = createDto.IsAvaliable,

                });
            }

            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _productDbSet.AnyAsync(product => product.Name == name);
        }

        public async Task<IEnumerable<IndexRowProductDto>> GetAllAsync()
        {
            return await _productDbSet
                .Select(product => new IndexRowProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    IsAvaliable = product.IsAvaliable,
                })
                .OrderBy(product => product.Name)
                .ToListAsync();
        }

        public async Task<DisplayProductDto> GetDisplayModelAsync(int id)
        {
            return await _productDbSet
                .Where(product => product.Id == id)
                .Select(product => new DisplayProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    IsAvaliable = product.IsAvaliable,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<EditProductDto> GetEditModelAsync(int id)
        {
            return await _productDbSet
                .Where(product => product.Id == id)
                .Select(product => new EditProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    IsAvaliable = product.IsAvaliable,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<int> GetIdFromNameAsync(string name)
        {
            return await _productDbSet
                .Where(product => product.Name == name)
                .Select(product => product.Id)
                .SingleOrDefaultAsync();
        }

        public Product UpdateFromObject(object value)
        {
            if (value is Product product) return Update(product);

            if (value is EditProductDto editDto)
            {
                return Update(new Product
                {
                    Id = editDto.Id,
                    Name = editDto.Name,
                    Description = editDto.Description,
                    Category = editDto.Category,
                    Price = editDto.Price,
                    IsAvaliable = editDto.IsAvaliable,

                });
            }

            throw new NotImplementedException();
        }
    }
}
