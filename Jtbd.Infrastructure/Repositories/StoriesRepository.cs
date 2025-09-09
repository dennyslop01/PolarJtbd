using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.Repositories
{
    public class StoriesRepository(JtbdDbContext context) : IStories
    {
        private readonly JtbdDbContext _context = context;
        public async Task<bool> CreateAsync(CreateStorie stories)
        {
            Stories auxStorie = new Stories();
            auxStorie.TitleStorie = stories.TitleStorie;
            auxStorie.ContextStorie = stories.ContextStorie;
            auxStorie.CreatedUser = stories.CreatedUser;
            auxStorie.CreatedDate = stories.CreatedDate;
            auxStorie.UpdatedDate = stories.UpdatedDate;
            auxStorie.UpdatedUser = stories.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == stories.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxStorie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var interview = _context.Interviews.Where(x => x.IdInter == stories.IdInter).AsQueryable().AsNoTracking().FirstOrDefault();
            if (interview != null)
            {
                auxStorie.IdInter = interview;
            }
            else
            {
                throw new InvalidOperationException("El entrevistado no existe.");
            }

            await _context.Stories.AddAsync(auxStorie);
            _context.Entry(auxStorie.Project).State = EntityState.Unchanged;
            _context.Entry(auxStorie.IdInter).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxStorie).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;
            _context.Entry(interview).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storie = await _context.Stories.FindAsync(id);
            if (storie != null)
            {
                _context.Stories.Remove(storie);
                await _context.SaveChangesAsync();
                _context.Entry(storie).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Stories>> GetAllAsync()
        {
            return await _context.Stories
               .Include(x => x.Project)
               .Include(x => x.IdInter)
               .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<Stories> GetByIdAsync(int id)
        {
            var storie = await _context.Stories
                 .Include(x => x.Project)
                 .Include(x => x.IdInter)
                 .FirstOrDefaultAsync(x => x.IdStorie == id);
            return storie!;
        }

        public async Task<IEnumerable<Stories>> GetByProjectIdAsync(int id)
        {
            var stories = await _context.Stories
                 .Include(x => x.Project)
                 .Include(x => x.IdInter)
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
            return stories!;
        }

        public async Task<bool> UpdateAsync(CreateStorie stories)
        {
            Stories auxStorie = new Stories();
            auxStorie.IdStorie = stories.IdStorie;
            auxStorie.TitleStorie = stories.TitleStorie;
            auxStorie.ContextStorie = stories.ContextStorie;
            auxStorie.CreatedUser = stories.CreatedUser;
            auxStorie.CreatedDate = stories.CreatedDate;
            auxStorie.UpdatedDate = stories.UpdatedDate;
            auxStorie.UpdatedUser = stories.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == stories.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxStorie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var interview = _context.Interviews.Where(x => x.IdInter == stories.IdInter).AsQueryable().AsNoTracking().FirstOrDefault();
            if (interview != null)
            {
                auxStorie.IdInter = interview;
            }
            else
            {
                throw new InvalidOperationException("El entrevistado no existe.");
            }

            _context.Stories.Update(auxStorie);
            _context.Entry(auxStorie.Project).State = EntityState.Unchanged;
            _context.Entry(auxStorie.IdInter).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxStorie).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;
            _context.Entry(interview).State = EntityState.Detached;
            return true;
        }
    }
}