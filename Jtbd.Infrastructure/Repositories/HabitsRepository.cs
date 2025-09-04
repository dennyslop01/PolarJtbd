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
        public async Task<bool> CreateAsync(CreateHabits habits)
        {
            Habits auxHabbit = new Habits();
            auxHabbit.HabitName = habits.HabitName;
            //auxHabbit.ha = habits.PullDescription;
            auxHabbit.StatusHabit = habits.StatusHabit;
            auxHabbit.CreatedUser = habits.CreatedUser;
            auxHabbit.CreatedDate = habits.CreatedDate;
            auxHabbit.UpdatedDate = habits.UpdatedDate;
            auxHabbit.UpdatedUser = habits.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == habits.IdProject).FirstOrDefault();
            if (project != null)
            {
                auxHabbit.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.Habits.AddAsync(auxHabbit);
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

        public async Task<IEnumerable<Habits>> GetByProjectIdAsync(int id)
        {
            var habbit = await _context.Habits
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).ToListAsync();
            return habbit!;
        }

        public async Task<bool> UpdateAsync(CreateHabits habits)
        {
            Habits auxHabbit = new Habits();
            auxHabbit.IdHabit = habits.IdHabit;
            auxHabbit.HabitName = habits.HabitName;
            //auxHabbit.ha = habits.PullDescription;
            auxHabbit.StatusHabit = habits.StatusHabit;
            auxHabbit.CreatedUser = habits.CreatedUser;
            auxHabbit.CreatedDate = habits.CreatedDate;
            auxHabbit.UpdatedDate = habits.UpdatedDate;
            auxHabbit.UpdatedUser = habits.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == habits.IdProject).FirstOrDefault();
            if (project != null)
            {
                auxHabbit.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.Habits.Update(auxHabbit);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
