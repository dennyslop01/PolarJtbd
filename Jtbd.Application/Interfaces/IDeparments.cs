using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IDeparments
    {
        Task<IEnumerable<Deparments>> GetAllAsync();
        Task<Deparments> GetByIdAsync(int id);
        Task<bool> CreateAsync(Deparments deparments);
        Task<bool> UpdateAsync(Deparments deparments);
        Task<bool> DeleteAsync(int id);
    }
}
