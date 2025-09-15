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
        public async Task<Habits> CreateAsync(CreateHabits habits)
        {
            Habits existdatos = _context.Habits.Where(c => c.HabitName.Contains(habits.HabitName)).AsQueryable().AsNoTracking().FirstOrDefault();
            if (existdatos != null)
                return (existdatos);


            Habits auxHabbit = new Habits();
            auxHabbit.HabitName = habits.HabitName;
            //auxHabbit.ha = habits.PullDescription;
            auxHabbit.StatusHabit = habits.StatusHabit;
            auxHabbit.CreatedUser = habits.CreatedUser;
            auxHabbit.CreatedDate = habits.CreatedDate;
            auxHabbit.UpdatedDate = habits.UpdatedDate;
            auxHabbit.UpdatedUser = habits.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == habits.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxHabbit.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.Habits.AddAsync(auxHabbit);
            _context.Entry(auxHabbit.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxHabbit).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return auxHabbit;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existdatos = _context.StoriesHabbits.Where(c => c.Habits.IdHabit == id).AsQueryable().AsNoTracking().ToList();
            if (existdatos != null)
                return true;

            var habbit = await _context.Habits.FindAsync(id);
            if (habbit != null)
            {
                _context.Habits.Remove(habbit);
                await _context.SaveChangesAsync();
                _context.Entry(habbit).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Habits>> GetAllAsync()
        {
            return await _context.Habits
               .Include(x => x.Project)
               .AsQueryable().AsNoTracking().ToListAsync();
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
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
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

            var project = _context.Projects.Where(x => x.IdProject == habits.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxHabbit.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.Habits.Update(auxHabbit);
            _context.Entry(auxHabbit.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxHabbit).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }
    }
}
