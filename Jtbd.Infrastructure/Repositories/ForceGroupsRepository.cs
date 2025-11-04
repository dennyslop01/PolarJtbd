using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;

namespace Jtbd.Infrastructure.Repositories
{
    public class ForceGroupsRepository(JtbdDbContext context) : IForceGroups
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(ForceGroups credential)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ForceGroups>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ForceGroups> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ForceGroups credential)
        {
            throw new NotImplementedException();
        }
    }
}
