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
    public class InterviewsRepository(JtbdDbContext context) : IInterviews
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Interviews interviews)
        {
            await _context.Interviews.AddAsync(interviews);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview != null)
            {
                _context.Interviews.Remove(interview);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Interviews>> GetAllAsync()
        {
            return await _context.Interviews.ToListAsync();
        }

        public async Task<Interviews> GetByIdAsync(int id)
        {
            var categoria = await _context.Interviews
                 .FirstOrDefaultAsync(x => x.IdInter == id);
            return categoria!;
        }

        public async Task<bool> UpdateAsync(Interviews interviews)
        {
            _context.Interviews.Update(interviews);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
