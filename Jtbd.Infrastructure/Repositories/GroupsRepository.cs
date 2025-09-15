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
    public class GroupsRepository(JtbdDbContext context) : IGroups
    {
        private readonly JtbdDbContext _context = context;
        public async Task<Groups> CreateAsync(CreateGroup group)
        {
            Groups auxGroup = new Groups();
            auxGroup.GroupName = group.GroupName;

            var project = _context.Projects.Where(x => x.IdProject == group.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxGroup.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.Groups.AddAsync(auxGroup);
            _context.Entry(auxGroup.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxGroup).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return auxGroup;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Group = await _context.Groups.FindAsync(id);
            if (Group != null)
            {
                _context.Groups.Remove(Group);
                await _context.SaveChangesAsync();
                _context.Entry(Group).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Groups>> GetAllAsync()
        {
            return await _context.Groups
                .Include(x => x.Project)
                .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<Groups> GetByIdAsync(int id)
        {
            var Group = await _context.Groups
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdGroup == id);
            return Group!;
        }

        public async Task<IEnumerable<Groups>> GetByProjectIdAsync(int id)
        {
            var Group = await _context.Groups
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
            return Group!;
        }

        public async Task<bool> UpdateAsync(CreateGroup Group)
        {
            Groups auxGroup = new Groups();
            auxGroup.IdGroup = Group.IdGroup;
            auxGroup.GroupName = Group.GroupName;

            var project = _context.Projects.Where(x => x.IdProject == Group.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxGroup.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.Groups.Update(auxGroup);
            _context.Entry(auxGroup.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxGroup).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }
    }
}
