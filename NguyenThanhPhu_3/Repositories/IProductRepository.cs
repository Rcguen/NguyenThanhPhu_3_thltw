using NguyenThanhPhu_3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using NguyenThanhPhu_3.Models;

namespace NguyenThanhPhu_3.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    }
}
