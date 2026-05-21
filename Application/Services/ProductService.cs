using MInimalApi.Core.Entities;
using MInimalApi.Core.Interfaces;

namespace MInimalApi.Application.Services
{
    public class ProductService
    {

        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(Product product) => _repo.AddAsync(product);
        public Task UpdateAsync(Product product) => _repo.UpdateAsync(product);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

    }
}
