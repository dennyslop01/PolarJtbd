using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IInterviews
    {
        Task<IEnumerable<Interviews>> GetAllAsync();
        Task<Interviews> GetByIdAsync(int id);
        Task<bool> CreateAsync(Interviews interviews);
        Task<bool> UpdateAsync(Interviews interviews);
        Task<bool> DeleteAsync(int id);
    }
}
