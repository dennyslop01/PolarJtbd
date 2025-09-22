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
        Task<Employee> GetByUsernameAsync(string user);
        Task<bool> CreateAsync(CreateEmployee employee);
        Task<bool> UpdateAsync(CreateEmployee employee);
        Task<bool> DeleteAsync(int id);
    }
}
