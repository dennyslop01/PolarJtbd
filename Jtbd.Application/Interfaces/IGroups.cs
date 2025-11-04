using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IGroups
    {
        Task<IEnumerable<Groups>> GetAllAsync();
        Task<Groups> GetByIdAsync(int id);
        Task<IEnumerable<Groups>> GetByProjectIdAsync(int id);
        Task<IEnumerable<Groups>> GetByProjectIdIndicadorAsync(int id, int indicador);
        Task<Groups> CreateAsync(CreateGroup pull);
        Task<bool> UpdateAsync(CreateGroup pull);
        Task<bool> DeleteAsync(int id);
    }
}
