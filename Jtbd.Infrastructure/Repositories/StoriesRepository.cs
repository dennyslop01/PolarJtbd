using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Infrastructure.DataContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.Repositories
{
    public class StoriesRepository(JtbdDbContext context) : IStories
    {
        private readonly JtbdDbContext _context = context;
        public async Task<bool> CreateAsync(CreateStorie stories)
        {
            Stories auxStorie = new Stories();
            auxStorie.TitleStorie = stories.TitleStorie;
            auxStorie.ContextStorie = stories.ContextStorie;
            auxStorie.CreatedUser = stories.CreatedUser;
            auxStorie.CreatedDate = stories.CreatedDate;
            auxStorie.UpdatedDate = stories.UpdatedDate;
            auxStorie.UpdatedUser = stories.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == stories.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxStorie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var interview = _context.Interviews.Where(x => x.IdInter == stories.IdInter).AsQueryable().AsNoTracking().FirstOrDefault();
            if (interview != null)
            {
                auxStorie.IdInter = interview;
            }
            else
            {
                throw new InvalidOperationException("El entrevistado no existe.");
            }

            await _context.Stories.AddAsync(auxStorie);
            _context.Entry(auxStorie.Project).State = EntityState.Unchanged;
            _context.Entry(auxStorie.IdInter).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxStorie).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;
            _context.Entry(interview).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var storie = await _context.Stories.FindAsync(id);
            if (storie != null)
            {
                _context.Stories.Remove(storie);
                await _context.SaveChangesAsync();
                _context.Entry(storie).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Stories>> GetAllAsync()
        {
            return await _context.Stories
               .Include(x => x.Project)
               .Include(x => x.IdInter)
               .AsQueryable().AsNoTracking().ToListAsync();
        }

        public async Task<Stories> GetByIdAsync(int id)
        {
            var storie = await _context.Stories
                 .Include(x => x.Project)
                 .Include(x => x.IdInter)
                 .FirstOrDefaultAsync(x => x.IdStorie == id);
            return storie!;
        }

        public async Task<IEnumerable<Stories>> GetByProjectIdAsync(int id)
        {
            var stories = await _context.Stories
                 .Include(x => x.Project)
                 .Include(x => x.IdInter)
                 .Where(x => x.Project.IdProject == id).AsQueryable().AsNoTracking().ToListAsync();
            return stories!;
        }

        public async Task<IEnumerable<Stories>> GetByInterIdAsync(int id)
        {
            var stories = await _context.Stories
                 .Include(x => x.Project)
                 .Include(x => x.IdInter)
                 .Where(x => x.IdInter.IdInter == id).AsQueryable().AsNoTracking().ToListAsync();
            return stories!;
        }

        public async Task<bool> UpdateAsync(CreateStorie stories)
        {
            Stories auxStorie = new Stories();
            auxStorie.IdStorie = stories.IdStorie;
            auxStorie.TitleStorie = stories.TitleStorie;
            auxStorie.ContextStorie = stories.ContextStorie;
            auxStorie.ObservacionStorie = stories.ObservacionStorie;
            auxStorie.CreatedUser = stories.CreatedUser;
            auxStorie.CreatedDate = stories.CreatedDate;
            auxStorie.UpdatedDate = stories.UpdatedDate;
            auxStorie.UpdatedUser = stories.UpdatedUser;

            var project = _context.Projects.Where(x => x.IdProject == stories.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxStorie.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var interview = _context.Interviews.Where(x => x.IdInter == stories.IdInter).AsQueryable().AsNoTracking().FirstOrDefault();
            if (interview != null)
            {
                auxStorie.IdInter = interview;
            }
            else
            {
                throw new InvalidOperationException("El entrevistado no existe.");
            }

            _context.Stories.Update(auxStorie);
            _context.Entry(auxStorie.Project).State = EntityState.Unchanged;
            _context.Entry(auxStorie.IdInter).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxStorie).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;
            _context.Entry(interview).State = EntityState.Detached;
            return true;
        }

        public async Task<IEnumerable<StoriesPush>> GetPushesByStorieIdAsync(int id)
        {
            var pushstorie = await _context.StoriesPushes
                .Include(x => x.Stories)
                .Include(x => x.PushesGroups)
                .Where(x => x.Stories.IdStorie == id).AsNoTracking().ToListAsync();

            return pushstorie!;
        }

        public async Task<IEnumerable<StoriesPull>> GetPullsByStorieIdAsync(int id)
        {
            var pullstorie = await _context.StoriesPulls
                .Include(x => x.Stories)
                .Include(x => x.PullGroups)
                .Where(x => x.Stories.IdStorie == id).AsNoTracking().ToListAsync();

            return pullstorie!;
        }

        public async Task<IEnumerable<StoriesHabbit>> GetHabitsByStorieIdAsync(int id)
        {
            var habitstorie = await _context.StoriesHabbits
                .Include(x => x.Stories)
                .Include(x => x.Habits)
                .Where(x => x.Stories.IdStorie == id).AsNoTracking().ToListAsync();

            return habitstorie!;
        }

        public async Task<IEnumerable<StoriesAnxiety>> GetAxieByStorieIdAsync(int id)
        {
            var anxiestorie = await _context.StoriesAnxieties
                .Include(x => x.Stories)
                .Include(x => x.Anxieties)
                .Where(x => x.Stories.IdStorie == id).AsNoTracking().ToListAsync();

            return anxiestorie!;
        }

        public async Task<bool> CreateStoriePushAsync(CreateStoriesPush nuevo)
        {
            var storie = _context.Stories.Where(x => x.IdStorie == nuevo.IdStories).AsQueryable().AsNoTracking().FirstOrDefault();
            if (storie == null)
            {
                throw new InvalidOperationException("La historia no existe.");
            }

            var push = _context.PushesGroups.Where(x => x.IdPush == nuevo.IdPush).AsQueryable().AsNoTracking().FirstOrDefault();
            if (push == null)
            {
                throw new InvalidOperationException("El push no existe.");
            }

            var result = await _context.Database.ExecuteSqlAsync(
                $"Insert into StoriesPushes (StoriesIdStorie, PushesGroupsIdPush) Values( {nuevo.IdStories}, {nuevo.IdPush})");

            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateStoriePullAsync(CreateStoriesPull nuevo)
        {
            var storie = _context.Stories.Where(x => x.IdStorie == nuevo.IdStories).AsQueryable().AsNoTracking().FirstOrDefault();
            if (storie == null)
            {
                throw new InvalidOperationException("La historia no existe.");
            }

            var pull = _context.PullGroups.Where(x => x.IdPull == nuevo.IdPull).AsQueryable().AsNoTracking().FirstOrDefault();
            if (pull == null)
            {
                throw new InvalidOperationException("El pull no existe.");
            }

            var result = await _context.Database.ExecuteSqlAsync(
                $"Insert into StoriesPulls (StoriesIdStorie, PullGroupsIdPull) Values( {nuevo.IdStories}, {nuevo.IdPull})");

            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateStorieHabitAsync(CreateStoriesHabbit nuevo)
        {
            var storie = _context.Stories.Where(x => x.IdStorie == nuevo.IdStories).AsQueryable().AsNoTracking().FirstOrDefault();
            if (storie == null)
            {
                throw new InvalidOperationException("La historia no existe.");
            }

            var habit = _context.Habits.Where(x => x.IdHabit == nuevo.IdHabit).AsQueryable().AsNoTracking().FirstOrDefault();
            if (habit == null)
            {
                throw new InvalidOperationException("El hábito no existe.");
            }

            var result = await _context.Database.ExecuteSqlAsync(
                $"Insert into StoriesHabbits (StoriesIdStorie, HabitsIdHabit) Values( {nuevo.IdStories}, {nuevo.IdHabit})");

            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateStorieAnxieAsync(CreateStoriesAnxiety nuevo)
        {
            var storie = _context.Stories.Where(x => x.IdStorie == nuevo.IdStories).AsQueryable().AsNoTracking().FirstOrDefault();
            if (storie == null)
            {
                throw new InvalidOperationException("La historia no existe.");
            }

            var anxie = _context.Anxieties.Where(x => x.IdAnxie == nuevo.IdAnxie).AsQueryable().AsNoTracking().FirstOrDefault();
            if (anxie == null)
            {
                throw new InvalidOperationException("El ansiedad no existe.");
            }

            var result = await _context.Database.ExecuteSqlAsync(
                $"Insert into StoriesAnxieties (StoriesIdStorie, AnxietiesIdAnxie) Values( {nuevo.IdStories}, {nuevo.IdAnxie})");

            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStorieEntityAsync(int opcion, int idStorie, int idEntidad)
        {
            switch (opcion)
            {
                case 1:
                    var result = await _context.Database.ExecuteSqlAsync($"DELETE StoriesPushes WHERE StoriesIdStorie={idStorie} AND PushesGroupsIdPush={idEntidad}");
                    break;
                case 2:
                    var result2 = await _context.Database.ExecuteSqlAsync($"DELETE StoriesPulls WHERE StoriesIdStorie={idStorie} AND PullGroupsIdPull={idEntidad}");
                    break;
                case 3:
                    var result3 = await _context.Database.ExecuteSqlAsync($"DELETE StoriesHabbits WHERE StoriesIdStorie={idStorie} AND HabitsIdHabit={idEntidad}");
                    break;
                case 4:
                    var result4 = await _context.Database.ExecuteSqlAsync($"DELETE StoriesAnxieties WHERE StoriesIdStorie={idStorie} AND AnxietiesIdAnxie={idEntidad}");
                    break;
            }

            //await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<StoriesPush>> GetPushesByProjectIdAsync(int id)
        {
            var pushstorie = await _context.StoriesPushes
                .Include(x => x.Groups)
                .Include(x => x.Stories)
                .Include(x => x.PushesGroups)
                .Where(x => x.PushesGroups.Project.IdProject == id).AsNoTracking().ToListAsync();

            return pushstorie!;
        }

        public async Task<bool> UpdatePushesGroupEntityAsync(int idPush, int? idGroup)
        {
            var result = await _context.Database.ExecuteSqlAsync($"UPDATE [StoriesPushes] SET [GroupsIdGroup] = {idGroup} WHERE [PushesGroupsIdPush] = {idPush}");
            return true;
        }

        public async Task<IEnumerable<StoriesPull>> GetPullsByProjectIdAsync(int id)
        {
            var pushstorie = await _context.StoriesPulls
                .Include(x => x.Groups)
                .Include(x => x.Stories)
                .Include(x => x.PullGroups)
                .Where(x => x.PullGroups.Project.IdProject == id).AsNoTracking().ToListAsync();

            return pushstorie!;
        }

        public async Task<bool> UpdatePullsGroupEntityAsync(int idPull, int? idGroup)
        {
            var result = await _context.Database.ExecuteSqlAsync($"UPDATE [StoriesPulls] SET [GroupsIdGroup] = {idGroup} WHERE [PullGroupsIdPull] = {idPull}");
            return true;
        }

        public async Task<byte[]> GenerarExcelMatrizAsync(string IdProyecto)
        {
            ProjectsRepository repoproyect = new ProjectsRepository(_context);
            GroupsRepository repogroups = new GroupsRepository(_context);

            var package = await repoproyect.GetByIdAsync(int.Parse(IdProyecto));

            Projects proyecto = await repoproyect.GetByIdAsync(int.Parse(IdProyecto));
            List<Groups> grupos = (List<Groups>)await repogroups.GetByProjectIdAsync(int.Parse(IdProyecto));
            List<Stories> stories = (List<Stories>)await GetByProjectIdAsync(int.Parse(IdProyecto));
            List<StoriesPull> storiespull = (List<StoriesPull>)await GetPullsByProjectIdAsync(int.Parse(IdProyecto));
            List<StoriesPush> storiespush = (List<StoriesPush>)await GetPushesByProjectIdAsync(int.Parse(IdProyecto));

            Console.Write(proyecto.ProjectName);

            Console.WriteLine(string.Empty);
            foreach (var p in grupos)
            {
                Console.WriteLine("Push");
            }
            foreach (var p in grupos)
            {
                Console.WriteLine("Pull");
            }
            foreach (var p in grupos)
            {
                Console.WriteLine(p.GroupName);
            }
            foreach (var p in grupos)
            {
                Console.WriteLine(p.GroupName);
            }

            foreach (var p in stories)
            {

                Console.WriteLine(p.IdInter.InterName + "-" + p.TitleStorie);
                foreach (var p1 in grupos)
                {
                    try
                    {
                        int contador = storiespush.Where(c => c.Stories?.IdStorie == p.IdStorie && c.Groups?.IdGroup == p1.IdGroup).Count();
                        if (contador > 0)
                        {
                            Console.WriteLine("1");
                        }
                        else
                        {
                            if (storiespush.Where(c => c.Stories?.IdStorie == p.IdStorie && c.Groups == null).Count() > 0)
                            {
                                Console.WriteLine("-");
                            }
                            else
                            {
                                Console.WriteLine("0");
                            }
                        }
                    }
                    catch { }
                }

                foreach (var p1 in grupos)
                {
                    try
                    {
                        int contador = storiespull.Where(c => c.Stories?.IdStorie == p.IdStorie && c.Groups?.IdGroup == p1.IdGroup).Count();
                        if (contador > 0)
                        {
                            Console.WriteLine("1");
                        }
                        else
                        {
                            if (storiespull.Where(c => c.Stories?.IdStorie == p.IdStorie && c.Groups == null).Count() > 0)
                            {
                                Console.WriteLine("-");
                            }
                            else
                            {
                                Console.WriteLine("0");
                            }
                        }
                    }
                    catch { }
                }
            }

            return null;// package.GetAsByteArray();
        }

        public async Task<IEnumerable<StoriesGroupsPushes>> GetStorieGroupPushesByProjectIdAsync(int id)
        {
            var pushstorie = await _context.StoriesGroupsPushes
                .Include(x => x.Groups)
                .Include(x => x.Stories)
                .Where(x => x.Stories.Project.IdProject == id).AsNoTracking().ToListAsync();

            return pushstorie!;
        }

        public async Task<IEnumerable<StoriesGroupsPulls>> GetStorieGroupPullsByProjectIdAsync(int id)
        {
            var pushstorie = await _context.StoriesGroupsPulls
                .Include(x => x.Groups)
                .Include(x => x.Stories)
                .Where(x => x.Stories.Project.IdProject == id).AsNoTracking().ToListAsync();

            return pushstorie!;
        }

        public async Task<bool> UpdateStorieValorAsync(int idstorie, int idgroup, int tipo, int valor)
        {
            if(tipo == 1)
            {
                var push = await _context.StoriesGroupsPushes.Where(x => x.Stories.IdStorie == idstorie && x.Groups.IdGroup == idgroup && x.Groups.IdTipo == 0).AsNoTracking().ToListAsync();
                if(push.Count() == 0)
                {
                    var result = await _context.Database.ExecuteSqlAsync($"INSERT INTO StoriesGroupsPushes (StoriesIdStorie,GroupsIdGroup,ValorPush) VALUES({idstorie},{idgroup},{valor})");
                }
                else
                {
                    var result = await _context.Database.ExecuteSqlAsync($"UPDATE StoriesGroupsPushes SET ValorPush = {valor} WHERE StoriesIdStorie = {idstorie} AND GroupsIdGroup = {idgroup}");
                }
            }
            else
            {
                var pull = await _context.StoriesGroupsPulls.Where(x => x.Stories.IdStorie == idstorie && x.Groups.IdGroup == idgroup && x.Groups.IdTipo == 1).AsNoTracking().ToListAsync();
                if (pull.Count() == 0)
                {
                    var result = await _context.Database.ExecuteSqlAsync($"INSERT INTO StoriesGroupsPulls (StoriesIdStorie,GroupsIdGroup,ValorPull) VALUES({idstorie},{idgroup},{valor})");
                }
                else
                {
                    var result = await _context.Database.ExecuteSqlAsync($"UPDATE StoriesGroupsPulls SET ValorPull = {valor} WHERE StoriesIdStorie = {idstorie} AND GroupsIdGroup = {idgroup}");
                }
            }
            return true;
        }
    }
}