using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IForceGroups
    {
        Task<IEnumerable<ForceGroups>> GetAllAsync();
        Task<ForceGroups> GetByIdAsync(int id);
        Task<bool> CreateAsync(ForceGroups force);
        Task<bool> UpdateAsync(ForceGroups force);
        Task<bool> DeleteAsync(int id);
    }
}
