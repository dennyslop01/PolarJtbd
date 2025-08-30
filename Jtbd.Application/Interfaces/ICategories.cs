using Jtbd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Application.Interfaces
{
    public interface ICategories
    {
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(int id);
        Task<bool> CreateAsync(Categories categories);
        Task<bool> UpdateAsync(Categories categories);
        Task<bool> DeleteAsync(int id);
    }
}
