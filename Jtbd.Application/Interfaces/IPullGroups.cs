using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IPullGroups
    {
        Task<IEnumerable<PullGroups>> GetAllAsync();
        Task<PullGroups> GetByIdAsync(int id);
        Task<IEnumerable<PullGroups>> GetByProjectIdAsync(int id);
        Task<bool> CreateAsync(PullGroups pull);
        Task<bool> UpdateAsync(PullGroups pull);
        Task<bool> DeleteAsync(int id);
    }
}
