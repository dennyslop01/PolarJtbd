using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface ICategories
    {
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(int id);
        Task<bool> CreateAsync(Categories categories);
        Task<bool> UpdateAsync(Categories categories);
        Task<bool> DeleteAsync(int id);
    }
}
