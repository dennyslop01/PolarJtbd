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
    public class StoriesRepository(JtbdDbContext context) : IStories
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Stories Interviews)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Stories>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Stories> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Stories Interviews)
        {
            throw new NotImplementedException();
        }
    }
}
