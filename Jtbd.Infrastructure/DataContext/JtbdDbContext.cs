using Jtbd.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Groups> Groups { get; set; }
        public DbSet<StoriesAnxiety> StoriesAnxieties { get; set; }
        public DbSet<StoriesHabbit> StoriesHabbits { get; set; }
        public DbSet<StoriesPull> StoriesPulls { get; set; }
        public DbSet<StoriesPush> StoriesPushes { get; set; }
        public DbSet<StoriesGroupsPushes> StoriesGroupsPushes { get; set; }
        public DbSet<StoriesGroupsPulls> StoriesGroupsPulls { get; set; }
        public DbSet<StoriesClusters> StoriesClusters { get; set; }
        public DbSet<StoriesClustersJobs> StoriesClustersJobs { get; set; }
    }
}
