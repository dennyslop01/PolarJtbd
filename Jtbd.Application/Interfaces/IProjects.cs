using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IProjects
    {
        Task<IEnumerable<Projects>> GetAllAsync();
        Task<Projects> GetByIdAsync(int id);
        Task<IEnumerable<Projects>> GetByDeparmentIdAsync(int id);
        Task<bool> CreateAsync(CreateProject projects);
        Task<bool> UpdateAsync(CreateProject projects);
        Task<bool> DeleteAsync(int id);
    }
}
