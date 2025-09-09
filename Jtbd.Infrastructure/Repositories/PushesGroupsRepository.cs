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
        public async Task<bool> CreateAsync(CreatePushes push)
        {
            PushesGroups auxPushes = new PushesGroups();
            auxPushes.PushName = push.PushName;
            auxPushes.PushDescription = push.PushDescription;
            auxPushes.StatusPush = push.StatusPush;
            auxPushes.CreatedUser = push.CreatedUser;
            auxPushes.CreatedDate = push.CreatedDate;
            auxPushes.UpdatedDate = push.UpdatedDate;
            auxPushes.UpdatedUser = push.UpdatedUser;
            
            var project = _context.Projects.Where(x => x.IdProject == push.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxPushes.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.PushesGroups.AddAsync(auxPushes);
            _context.Entry(auxPushes.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxPushes).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var push = await _context.PushesGroups.FindAsync(id);
            if (push != null)
            {
                _context.PushesGroups.Remove(push);
                await _context.SaveChangesAsync();
                _context.Entry(push).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PushesGroups>> GetAllAsync()
        {
            return await _context.PushesGroups
               .Include(x => x.Project)
               .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<PushesGroups> GetByIdAsync(int id)
        {
            var push = await _context.PushesGroups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdPush == id);
            return push!;
        }

        public async Task<IEnumerable<PushesGroups>> GetByProjectIdAsync(int id)
        {
            var push = await _context.PushesGroups
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
            return push!;
        }

        public async Task<bool> UpdateAsync(CreatePushes push)
        {
            PushesGroups auxPushes = new PushesGroups();
            auxPushes.IdPush = push.IdPush;
            auxPushes.PushName = push.PushName;
            auxPushes.PushDescription = push.PushDescription;
            auxPushes.StatusPush = push.StatusPush;
            auxPushes.CreatedUser = push.CreatedUser;
            auxPushes.CreatedDate = push.CreatedDate;
            auxPushes.UpdatedDate = push.UpdatedDate;
            auxPushes.UpdatedUser = push.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == push.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxPushes.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.PushesGroups.Update(auxPushes);
            _context.Entry(auxPushes.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxPushes).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }
    }
}
