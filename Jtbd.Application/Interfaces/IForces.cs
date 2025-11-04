using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IForces
    {
        Task<IEnumerable<Forces>> GetAllAsync();
        Task<Forces> GetByIdAsync(int id);
        Task<bool> CreateAsync(Forces forces);
        Task<bool> UpdateAsync(Forces forces);
        Task<bool> DeleteAsync(int id);
    }
}
