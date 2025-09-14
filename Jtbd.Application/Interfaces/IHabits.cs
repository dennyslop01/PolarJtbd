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
        Task<IEnumerable<Habits>> GetByProjectIdAsync(int id);
        Task<Habits> CreateAsync(CreateHabits habits);
        Task<bool> UpdateAsync(CreateHabits habits);
        Task<bool> DeleteAsync(int id);
    }
}
