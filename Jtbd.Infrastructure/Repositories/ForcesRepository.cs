using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.Repositories
{
    public class ForcesRepository(JtbdDbContext context) : IForces
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Forces credential)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Forces>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Forces> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Forces credential)
        {
            throw new NotImplementedException();
        }
    }
}
