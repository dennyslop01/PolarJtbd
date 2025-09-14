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
        public async Task<PullGroups> CreateAsync(CreatePull pull)
        {
            PullGroups auxPull = new PullGroups();
            auxPull.PullName = pull.PullName;
            auxPull.PullDescription = pull.PullDescription;
            auxPull.StatusPull = pull.StatusPull;
            auxPull.CreatedUser = pull.CreatedUser;
            auxPull.CreatedDate = pull.CreatedDate;
            auxPull.UpdatedDate = pull.UpdatedDate;
            auxPull.UpdatedUser = pull.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == pull.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxPull.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.PullGroups.AddAsync(auxPull);
            _context.Entry(auxPull.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxPull).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return auxPull;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pull = await _context.PullGroups.FindAsync(id);
            if (pull != null)
            {
                _context.PullGroups.Remove(pull);
                await _context.SaveChangesAsync();
                _context.Entry(pull).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PullGroups>> GetAllAsync()
        {
            return await _context.PullGroups
                .Include(x => x.Project)
                .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<PullGroups> GetByIdAsync(int id)
        {
            var pull = await _context.PullGroups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdPull == id);
            return pull!;
        }

        public async Task<IEnumerable<PullGroups>> GetByProjectIdAsync(int id)
        {
            var pull = await _context.PullGroups
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
            return pull!;
        }

        public async Task<bool> UpdateAsync(CreatePull pull)
        {
            PullGroups auxPull = new PullGroups();
            auxPull.IdPull = pull.IdPull;
            auxPull.PullName = pull.PullName;
            auxPull.PullDescription = pull.PullDescription;
            auxPull.StatusPull = pull.StatusPull;
            auxPull.CreatedUser = pull.CreatedUser;
            auxPull.CreatedDate = pull.CreatedDate;
            auxPull.UpdatedDate = pull.UpdatedDate;
            auxPull.UpdatedUser = pull.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == pull.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxPull.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.PullGroups.Update(auxPull);
            _context.Entry(auxPull.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxPull).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }
    }
}
