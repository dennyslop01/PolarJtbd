using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IPushesGroups
    {
        Task<IEnumerable<PushesGroups>> GetAllAsync();
        Task<PushesGroups> GetByIdAsync(int id);
        Task<IEnumerable<PushesGroups>> GetByProjectIdAsync(int id);
        Task<PushesGroups> CreateAsync(CreatePushes push);
        Task<bool> UpdateAsync(CreatePushes push);
        Task<bool> DeleteAsync(int id);
    }
}
