using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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
