using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IInterviews
    {
        Task<IEnumerable<Interviews>> GetAllAsync();
        Task<Interviews> GetByIdAsync(int id);
        Task<IEnumerable<Interviews>> GetByProjectIdAsync(int id);
        Task<bool> CreateAsync(CreateInterview interviews);
        Task<bool> UpdateAsync(CreateInterview interviews);
        Task<bool> DeleteAsync(int id);
    }
}
