using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface IGroups
    {
        Task<IEnumerable<Groups>> GetAllAsync();
        Task<Groups> GetByIdAsync(int id);
        Task<IEnumerable<Groups>> GetByProjectIdAsync(int id);
        Task<IEnumerable<Groups>> GetByProjectIdIndicadorAsync(int id, int indicador);
        Task<Groups> CreateAsync(CreateGroup pull);
        Task<bool> UpdateAsync(CreateGroup pull);
        Task<bool> DeleteAsync(int id);
    }
}
