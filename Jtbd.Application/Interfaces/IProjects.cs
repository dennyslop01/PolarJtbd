using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IProjects
    {
        Task<IEnumerable<Projects>> GetAllAsync();
        Task<Projects> GetByIdAsync(int id);
        Task<Projects> GetByDeparmentIdAsync(int id);
        Task<bool> CreateAsync(CreateProject projects);
        Task<bool> UpdateAsync(CreateProject projects);
        Task<bool> DeleteAsync(int id);
    }
}
