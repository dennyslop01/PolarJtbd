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
    public class AnxietiesRepository(JtbdDbContext context) : IAnxieties
    {
        private readonly JtbdDbContext _context = context;
        public async Task<bool> CreateAsync(CreateAnxietie anxieties)
        {
            Anxieties auxAnxie = new Anxieties();
            auxAnxie.AnxieName = anxieties.AnxieName;
            //auxHabbit.ha = habits.PullDescription;
            auxAnxie.StatusAnxie = anxieties.StatusAnxie;
            auxAnxie.CreatedUser = anxieties.CreatedUser;
            auxAnxie.CreatedDate = anxieties.CreatedDate;
            auxAnxie.UpdatedDate = anxieties.UpdatedDate;
            auxAnxie.UpdatedUser = anxieties.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == anxieties.IdProject).FirstOrDefault();
            if (project != null)
            {
                auxAnxie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            await _context.Anxieties.AddAsync(auxAnxie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var anxiet = await _context.Anxieties.FindAsync(id);
            if (anxiet != null)
            {
                _context.Anxieties.Remove(anxiet);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Anxieties>> GetAllAsync()
        {
            return await _context.Anxieties
               .Include(x => x.Project)
               .ToListAsync();
        }

        public async Task<Anxieties> GetByIdAsync(int id)
        {
            var anxies = await _context.Anxieties
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdAnxie == id);
            return anxies!;
        }

        public async Task<IEnumerable<Anxieties>> GetByProjectIdAsync(int id)
        {
            var anxies = await _context.Anxieties
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).ToListAsync();
            return anxies!;
        }

        public async Task<bool> UpdateAsync(CreateAnxietie anxieties)
        {
            Anxieties auxAnxie = new Anxieties();
            auxAnxie.IdAnxie = anxieties.IdAnxie;
            auxAnxie.AnxieName = anxieties.AnxieName;
            //auxHabbit.ha = habits.PullDescription;
            auxAnxie.StatusAnxie = anxieties.StatusAnxie;
            auxAnxie.CreatedUser = anxieties.CreatedUser;
            auxAnxie.CreatedDate = anxieties.CreatedDate;
            auxAnxie.UpdatedDate = anxieties.UpdatedDate;
            auxAnxie.UpdatedUser = anxieties.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == anxieties.IdProject).FirstOrDefault();
            if (project != null)
            {
                auxAnxie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }
            _context.Anxieties.Update(auxAnxie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
