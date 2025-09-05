using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IStories
    {
        Task<IEnumerable<Stories>> GetAllAsync();
        Task<Stories> GetByIdAsync(int id);
        Task<IEnumerable<Stories>> GetByProjectIdAsync(int id);
        Task<bool> CreateAsync(CreateStorie stories);
        Task<bool> UpdateAsync(CreateStorie stories);
        Task<bool> DeleteAsync(int id);
    }
}
