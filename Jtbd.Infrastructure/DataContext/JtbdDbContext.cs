using Jtbd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Jtbd.Infrastructure.DataContext
{
    public class JtbdDbContext(DbContextOptions<JtbdDbContext> options) : DbContext(options)
    {
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Deparments> Deparments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Interviews> Interviews { get; set; }
        public DbSet<PushesGroups> PushesGroups { get; set; }
        public DbSet<PullGroups> PullGroups { get; set; }
        public DbSet<Anxieties> Anxieties { get; set; }
        public DbSet<Habits> Habits { get; set; }
        public DbSet<Stories> Stories { get; set; }
        public DbSet<StoriesAnxiety> StoriesAnxiety { get; set; }
        public DbSet<StoriesHabbit> StoriesHabbit { get; set; }
        public DbSet<StoriesPull> StoriesPull { get; set; }
        public DbSet<StoriesPush> StoriesPush { get; set; }
    }
}
