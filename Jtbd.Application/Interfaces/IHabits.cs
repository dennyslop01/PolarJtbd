using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IHabits
    {
        Task<IEnumerable<Habits>> GetAllAsync();
        Task<Habits> GetByIdAsync(int id);
        Task<Habits> GetByProjectIdAsync(int id);
        Task<bool> CreateAsync(Habits habits);
        Task<bool> UpdateAsync(Habits habits);
        Task<bool> DeleteAsync(int id);
    }
}
