using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.DataContext
{
    internal class JtbdDbContextFactory : IDesignTimeDbContextFactory<JtbdDbContext>
    {
        private readonly IConfiguration _configuration;
        public JtbdDbContextFactory(IConfiguration confi)
        {
            _configuration = confi;
        }

        public JtbdDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<JtbdDbContext>();
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            return new JtbdDbContext(optionBuilder.Options);

        }
    }
}
