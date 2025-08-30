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
    public class HabitsRepository(JtbdDbContext context) : IHabits
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Habits habits)
        {
            var project = await _context.Projects.FindAsync(habits.Project!.IdProject);
            if (project != null)
            {
                habits.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.Habits.AddAsync(habits);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var habbit = await _context.Habits.FindAsync(id);
            if (habbit != null)
            {
                _context.Habits.Remove(habbit);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Habits>> GetAllAsync()
        {
            return await _context.Habits
               .Include(x => x.Project)
               .ToListAsync();
        }

        public async Task<Habits> GetByIdAsync(int id)
        {
            var habbit = await _context.Habits
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdHabit == id);
            return habbit!;
        }

        public async Task<Habits> GetByProjectIdAsync(int id)
        {
            var habbit = await _context.Habits
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.Project.IdProject == id);
            return habbit!;
        }

        public async Task<bool> UpdateAsync(Habits habits)
        {
            var project = await _context.Projects.FindAsync(habits.Project!.IdProject);
            if (project != null)
            {
                habits.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.Habits.Update(habits);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
