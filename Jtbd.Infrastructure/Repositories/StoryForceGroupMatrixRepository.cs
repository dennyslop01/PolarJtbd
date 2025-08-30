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
    public class StoryForceGroupMatrixRepository(JtbdDbContext context) : IStoryForceGroupMatrix
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(StoryForceGroupMatrix matrix)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StoryForceGroupMatrix>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<StoryForceGroupMatrix> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(StoryForceGroupMatrix matrix)
        {
            throw new NotImplementedException();
        }
    }
}
