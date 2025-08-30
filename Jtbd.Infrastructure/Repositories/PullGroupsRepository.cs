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
    public class PullGroupsRepository(JtbdDbContext context) : IPullGroups
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(PullGroups pull)
        {
            var project = await _context.Projects.FindAsync(pull.Project!.IdProject);
            if (project != null)
            {
                pull.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.PullGroups.AddAsync(pull);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pull = await _context.PullGroups.FindAsync(id);
            if (pull != null)
            {
                _context.PullGroups.Remove(pull);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PullGroups>> GetAllAsync()
        {
            return await _context.PullGroups
                .Include(x => x.Project)
                .ToListAsync();
        }

        public async Task<PullGroups> GetByIdAsync(int id)
        {
            var pull = await _context.PullGroups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdPull == id);
            return pull!;
        }

        public async Task<PullGroups> GetByProjectIdAsync(int id)
        {
            var pull = await _context.PullGroups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.Project.IdProject == id);
            return pull!;
        }

        public async Task<bool> UpdateAsync(PullGroups pull)
        {
            var project = await _context.Projects.FindAsync(pull.Project!.IdProject);
            if (project != null)
            {
                pull.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.PullGroups.Update(pull);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
