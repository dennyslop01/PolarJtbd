using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
