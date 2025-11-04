using Jtbd.Domain.Entities;

namespace Jtbd.Application.Interfaces
{
    public interface IStoryForceGroupMatrix
    {
        Task<IEnumerable<StoryForceGroupMatrix>> GetAllAsync();
        Task<StoryForceGroupMatrix> GetByIdAsync(int id);
        Task<bool> CreateAsync(StoryForceGroupMatrix matrix);
        Task<bool> UpdateAsync(StoryForceGroupMatrix matrix);
        Task<bool> DeleteAsync(int id);
    }
}
