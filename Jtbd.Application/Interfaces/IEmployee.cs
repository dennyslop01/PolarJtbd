using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<bool> CreateAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}
