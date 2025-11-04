using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IPullGroups
    {
        Task<IEnumerable<PullGroups>> GetAllAsync();
        Task<PullGroups> GetByIdAsync(int id);
        Task<IEnumerable<PullGroups>> GetByProjectIdAsync(int id);
        Task<PullGroups> CreateAsync(CreatePull pull);
        Task<bool> UpdateAsync(CreatePull pull);
        Task<bool> DeleteAsync(int id);
    }
}
