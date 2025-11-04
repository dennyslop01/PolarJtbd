using Jtbd.Domain.Entities;

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
