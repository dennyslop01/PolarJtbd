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
        public async Task<bool> CreateAsync(CreateEmployee employee)
        {
            Employee emplee = new Employee();
            emplee.EmployeeName = employee.EmployeeName;
            emplee.EmployeeRol = employee.EmployeeRol;
            emplee.StatusEmployee = employee.StatusEmployee;

            Deparments? deparment = _context.Deparments.Where(x => x.Id == employee.IdDeparment).FirstOrDefault();
            if (deparment != null)
            {
                emplee.Deparments = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }
            await _context.Employees.AddAsync(emplee);
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

        public async Task<bool> UpdateAsync(CreateEmployee employee)
        {
            Employee emplee = new Employee();
            emplee.Id = employee.Id;
            emplee.EmployeeName = employee.EmployeeName;
            emplee.EmployeeRol = employee.EmployeeRol;
            emplee.StatusEmployee = employee.StatusEmployee;

            Deparments? deparment = _context.Deparments.Where(x => x.Id == employee.IdDeparment).FirstOrDefault();
            if (deparment != null)
            {
                emplee.Deparments = deparment;
            }
            else
            {
                throw new InvalidOperationException("El departamento no existe.");
            }

            _context.Employees.Update(emplee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
