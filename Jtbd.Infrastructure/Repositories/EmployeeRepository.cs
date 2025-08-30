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
    public class EmployeeRepository(JtbdDbContext context) : IEmployee
    {
        private readonly JtbdDbContext _context = context; 
        public async Task<bool> CreateAsync(Employee employee)
        {
            var deparment = await _context.Deparments.FindAsync(employee.Deparments!.Id);
            if (deparment != null)
            {
                employee.Deparments = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(x => x.Deparments)
                .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _context.Employees
                 .Include(x => x.Deparments)
                 .FirstOrDefaultAsync(x => x.Id == id);
            return employee!;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var deparment = await _context.Deparments.FindAsync(employee.Deparments!.Id);
            if (deparment != null)
            {
                employee.Deparments = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
