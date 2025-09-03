using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IPushesGroups
    {
        Task<IEnumerable<PushesGroups>> GetAllAsync();
        Task<PushesGroups> GetByIdAsync(int id);
        Task<IEnumerable<PushesGroups>> GetByProjectIdAsync(int id);
        Task<bool> CreateAsync(CreatePushes push);
        Task<bool> UpdateAsync(CreatePushes push);
        Task<bool> DeleteAsync(int id);
    }
}
