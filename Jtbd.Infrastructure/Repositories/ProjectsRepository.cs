using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

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
            await DeleteProyectDependencyAsync(id);

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

        private async Task<bool> DeleteProyectDependencyAsync(int proyectId)
        {
            var result = await _context.Database.ExecuteSqlAsync(
                $"Delete from StoriesClustersJobs Where ProjectIdProject = {proyectId}");

            var result2 = await _context.Database.ExecuteSqlAsync(
                $"Delete from StoriesClusters Where ProjectIdProject = {proyectId}");

            var result3 = await _context.Database.ExecuteSqlAsync(
                $"Delete from Anxieties Where ProjectIdProject = {proyectId}");

            var result4 = await _context.Database.ExecuteSqlAsync(
                $"Delete from Groups Where ProjectIdProject = {proyectId}");

            var result5 = await _context.Database.ExecuteSqlAsync(
                $"Delete from Habits Where ProjectIdProject = {proyectId}");

            var result7 = await _context.Database.ExecuteSqlAsync(
                $"Delete from PullGroups Where ProjectIdProject = {proyectId}");

            var result8 = await _context.Database.ExecuteSqlAsync(
                $"Delete from PushesGroups Where ProjectIdProject = {proyectId}");

            var result9 = await _context.Database.ExecuteSqlAsync(
                $"Delete from Stories Where ProjectIdProject = {proyectId}");

            var result6 = await _context.Database.ExecuteSqlAsync(
                $"Delete from Interviews Where ProjectIdProject = {proyectId}");

            return true;
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

        public async Task<IEnumerable<Projects>> GetByDeparmentIdAsync(int id)
        {
            return await _context.Projects
                .Include(x => x.Categories)
                .Include(x => x.Deparment)
                .AsQueryable().AsNoTracking().ToListAsync();
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
