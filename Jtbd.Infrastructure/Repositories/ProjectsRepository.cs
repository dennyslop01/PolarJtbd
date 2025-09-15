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
    public class ProjectsRepository(JtbdDbContext context) : IProjects
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(CreateProject project)
        {
            Projects auxpro = new Projects();

            var categoria = _context.Categories.Where(x => x.Id == project.idCategoria).AsQueryable().AsNoTracking().FirstOrDefault();
            if (categoria != null)
            {
                auxpro.Categories = categoria;
            }
            else
            {
                throw new InvalidOperationException("La categoría no existe.");
            }
            var deparment = _context.Deparments.Where(x => x.Id == project.IdDeparmento).AsQueryable().AsNoTracking().FirstOrDefault();
            if (deparment != null)
            {
                auxpro.Deparment = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }

            auxpro.ProjectName = project.ProjectName;
            auxpro.ProjectDate = project.ProjectDate;
            auxpro.ProjectDescription = project.ProjectDescription;
            auxpro.MaxPushes   = 0;// project.MaxPushes;
            auxpro.MaxPulls    = 0;// project.MaxPulls;
            auxpro.RutaImage   = project.RutaImage;
            auxpro.CreatedDate = project.CreatedDate;
            auxpro.CreatedUser = project.CreatedUser;
            auxpro.UpdatedDate = project.UpdatedDate;
            auxpro.UpdatedUser = project.UpdatedUser;
            auxpro.StatusProject = 1;// project.StatusProject;

            await _context.Projects.AddAsync(auxpro);
            _context.Entry(auxpro.Categories).State = EntityState.Unchanged;
            _context.Entry(auxpro.Deparment).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxpro).State = EntityState.Detached;
            _context.Entry(deparment).State = EntityState.Detached;
            _context.Entry(categoria).State = EntityState.Detached;
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                _context.Entry(project).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Projects>> GetAllAsync()
        {
            return await _context.Projects
                .Include(x => x.Categories)
                .Include(x => x.Deparment)
                .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<Projects> GetByIdAsync(int id)
        {
            var producto = await _context.Projects
                 .Include(x => x.Categories)
                 .Include(x => x.Deparment)
                 .FirstOrDefaultAsync(x => x.IdProject == id);
            return producto!;
        }

        public async Task<Projects> GetByDeparmentIdAsync(int id)
        {
            var producto = await _context.Projects
                 .Include(x => x.Categories)
                 .Include(x => x.Deparment)
                 .FirstOrDefaultAsync(x => x.Deparment.Id == id);
            return producto!;
        }

        public async Task<bool> UpdateAsync(CreateProject project)
        {
            Projects auxpro = new Projects();

            var categoria = _context.Categories.Where(x => x.Id == project.idCategoria).AsQueryable().AsNoTracking().FirstOrDefault();
            if (categoria != null)
            {
                auxpro.Categories = categoria;
            }
            else
            {
                throw new InvalidOperationException("La categoría no existe.");
            }
            var deparment = _context.Deparments.Where(x => x.Id == project.IdDeparmento).AsQueryable().AsNoTracking().FirstOrDefault();
            if (deparment != null)
            {
                auxpro.Deparment = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }

            auxpro.IdProject = project.IdProject;
            auxpro.ProjectName = project.ProjectName;
            auxpro.ProjectDate = project.ProjectDate;
            auxpro.ProjectDescription = project.ProjectDescription;
            auxpro.MaxPushes = project.MaxPushes;
            auxpro.MaxPulls = project.MaxPulls;
            auxpro.RutaImage = project.RutaImage;
            auxpro.CreatedDate = project.CreatedDate;
            auxpro.CreatedUser = project.CreatedUser;
            auxpro.UpdatedDate = project.UpdatedDate;
            auxpro.UpdatedUser = project.UpdatedUser;
            auxpro.StatusProject = project.StatusProject;

            _context.Projects.Update(auxpro);
            _context.Entry(auxpro.Categories).State = EntityState.Unchanged;
            _context.Entry(auxpro.Deparment).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxpro).State = EntityState.Detached;
            _context.Entry(deparment).State = EntityState.Detached;
            _context.Entry(categoria).State = EntityState.Detached;
            return true;
        }
    }
}
