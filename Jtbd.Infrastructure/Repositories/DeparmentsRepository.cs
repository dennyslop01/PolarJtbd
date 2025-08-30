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
    public class DeparmentsRepository(JtbdDbContext context) : IDeparments
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Deparments deparments)
        {
            await _context.Deparments.AddAsync(deparments);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Deparments.FindAsync(id);
            if (department != null)
            {
                _context.Deparments.Remove(department);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Deparments>> GetAllAsync()
        {
            return await _context.Deparments.ToListAsync();
        }

        public async Task<Deparments> GetByIdAsync(int id)
        {
            var categoria = await _context.Deparments
                  .FirstOrDefaultAsync(x => x.Id == id);
            return categoria!;
        }

        public async Task<bool> UpdateAsync(Deparments deparments)
        {
            _context.Deparments.Update(deparments);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
