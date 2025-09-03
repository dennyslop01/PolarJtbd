using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IAnxieties
    {
        Task<IEnumerable<Anxieties>> GetAllAsync();
        Task<Anxieties> GetByIdAsync(int id);
        Task<IEnumerable<Anxieties>> GetByProjectIdAsync(int id);
        Task<bool> CreateAsync(Anxieties anxieties);
        Task<bool> UpdateAsync(Anxieties anxieties);
        Task<bool> DeleteAsync(int id);
    }
}
