using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.Repositories
{
    public class PushesGroupsRepository(JtbdDbContext context) : IPushesGroups
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(PushesGroups push)
        {
            var project = await _context.Projects.FindAsync(push.Project!.IdProject);
            if (project != null)
            {
                push.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.PushesGroups.AddAsync(push);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var push = await _context.PushesGroups.FindAsync(id);
            if (push != null)
            {
                _context.PushesGroups.Remove(push);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PushesGroups>> GetAllAsync()
        {
            return await _context.PushesGroups
               .Include(x => x.Project)
               .ToListAsync();
        }

        public async Task<PushesGroups> GetByIdAsync(int id)
        {
            var push = await _context.PushesGroups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdPush == id);
            return push!;
        }

        public async Task<PushesGroups> GetByProjectIdAsync(int id)
        {
            var push = await _context.PushesGroups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.Project.IdProject == id);
            return push!;
        }

        public async Task<bool> UpdateAsync(PushesGroups push)
        {
            var project = await _context.Projects.FindAsync(push.Project!.IdProject);
            if (project != null)
            {
                push.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.PushesGroups.Update(push);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
