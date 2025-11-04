using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Jtbd.Infrastructure.Repositories
{
    public class CategoriesRepository(JtbdDbContext context) : ICategories
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Categories categories)
        {
            await _context.Categories.AddAsync(categories);
            await _context.SaveChangesAsync();
            _context.Entry(categories).State = EntityState.Detached;
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _context.Categories.FindAsync(id);
            if (categoria != null)
            {
                _context.Categories.Remove(categoria);
                await _context.SaveChangesAsync();
                _context.Entry(categoria).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            return await _context.Categories.AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<Categories> GetByIdAsync(int id)
        {
            var categoria = await _context.Categories
                 .AsQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return categoria!;
        }

        public async Task<bool> UpdateAsync(Categories categories)
        {
            _context.Categories.Update(categories);
            await _context.SaveChangesAsync();
            _context.Entry(categories).State = EntityState.Detached;
            return true;
        }
    }
}
