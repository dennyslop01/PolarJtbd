using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Jtbd.Infrastructure.Repositories
{
    public class InterviewsRepository(JtbdDbContext context) : IInterviews
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<Interviews> CreateAsync(CreateInterview interviews)
        {
            Interviews auxInter = new Interviews()
            {
                IdInter = interviews.IdInter,
                InterName = interviews.InterName,
                InterAge = interviews.InterAge,
                InterGender = interviews.InterGender,
                InterOccupation = interviews.InterOccupation,
                InterNickname = interviews.InterNickname,
                InterNSE = interviews.InterNSE,
                DateInter = interviews.DateInter
            };
            var project = _context.Projects.Where(x => x.IdProject == interviews.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxInter.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            await _context.Interviews.AddAsync(auxInter);
            _context.Entry(auxInter.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxInter).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;
            return auxInter;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview != null)
            {
                _context.Interviews.Remove(interview);
                await _context.SaveChangesAsync();
                _context.Entry(interview).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Interviews>> GetAllAsync()
        {
            return await _context.Interviews
                .Include(x => x.Project)
                .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<Interviews> GetByIdAsync(int id)
        {
            var inter = await _context.Interviews
                .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdInter == id);
            return inter!;
        }

        public async Task<IEnumerable<Interviews>> GetByProjectIdAsync(int id)
        {
            var inter = await _context.Interviews
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
            return inter!;
        }

        public async Task<bool> UpdateAsync(CreateInterview interviews)
        {
            Interviews auxInter = new Interviews()
            {
                IdInter = interviews.IdInter,
                InterName = interviews.InterName,
                InterAge = interviews.InterAge,
                InterGender = interviews.InterGender,
                InterOccupation = interviews.InterOccupation,
                InterNickname = interviews.InterNickname,
                InterNSE = interviews.InterNSE,
                DateInter = interviews.DateInter
            };

            var project = _context.Projects.Where(x => x.IdProject == interviews.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxInter.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            _context.Interviews.Update(auxInter);
            _context.Entry(auxInter.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxInter).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }
    }
}
