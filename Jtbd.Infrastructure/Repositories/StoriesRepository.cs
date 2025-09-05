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

            var project = _context.Projects.Where(x => x.IdProject == stories.IdProject).FirstOrDefault();
            if (project != null)
            {
                auxStorie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var interview = _context.Interviews.Where(x => x.IdInter == stories.IdInter).FirstOrDefault();
            if (interview != null)
            {
                auxStorie.Interviews = interview;
            }
            else
            {
                throw new InvalidOperationException("El entrevistado no existe.");
            }

            await _context.Stories.AddAsync(auxStorie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var anxiet = await _context.Stories.FindAsync(id);
            if (anxiet != null)
            {
                _context.Stories.Remove(anxiet);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Stories>> GetAllAsync()
        {
            return await _context.Stories
               .Include(x => x.Project)
               .ToListAsync();
        }

        public async Task<Stories> GetByIdAsync(int id)
        {
            var storie = await _context.Stories
                 .Include(x => x.Project)
                 .FirstOrDefaultAsync(x => x.IdStorie == id);
            return storie!;
        }

        public async Task<IEnumerable<Stories>> GetByProjectIdAsync(int id)
        {
            var stories = await _context.Stories
                 .Include(x => x.Project)
                 .Where(x => x.Project.IdProject == id).ToListAsync();
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

            var project = _context.Projects.Where(x => x.IdProject == stories.IdProject).FirstOrDefault();
            if (project != null)
            {
                auxStorie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var interview = _context.Interviews.Where(x => x.IdInter == stories.IdInter).FirstOrDefault();
            if (interview != null)
            {
                auxStorie.Interviews = interview;
            }
            else
            {
                throw new InvalidOperationException("El entrevistado no existe.");
            }

            _context.Stories.Update(auxStorie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}