using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
