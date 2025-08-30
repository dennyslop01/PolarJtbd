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
        public async Task<bool> CreateAsync(Projects project)
        {
            var categoria = await _context.Categories.FindAsync(project.Categories!.Id);
            if (categoria != null)
            {
                project.Categories = categoria;
            }
            else
            {
                throw new InvalidOperationException("La categoría no existe.");
            }
            var deparment = await _context.Deparments.FindAsync(project.Deparment!.Id);
            if (deparment != null)
            {
                project.Deparment = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Projects>> GetAllAsync()
        {
            return await _context.Projects
                .Include(x => x.Categories)
                .Include(x => x.Deparment)
                .ToListAsync();
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

        public async Task<bool> UpdateAsync(Projects project)
        {
            var categoria = await _context.Categories.FindAsync(project.Categories!.Id);
            if (categoria != null)
            {
                project.Categories = categoria;
            }
            else
            {
                throw new InvalidOperationException("La categoría no existe.");
            }
            var deparment = await _context.Deparments.FindAsync(project.Deparment!.Id);
            if (deparment != null)
            {
                project.Deparment = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
