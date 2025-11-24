using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Domain.ViewModel;
using Jtbd.Infrastructure.DataContext;
using Jtbd.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Jtbd.Infrastructure.Repositories
{
    public class MatrizWardRepository(JtbdDbContext context) : IMatrizWard
    {
        private readonly JtbdDbContext _context = context;

        public async Task<List<FinalClusterGroup>> GetMatrizWardAsync(int proyectId, int numberOfClusters)
        {
            await DeleteStorieClustereAsync(proyectId);

            // ===================================================================
            // PASO 1: DATOS DE ENTRADA (Matriz del proyecto)
            // ===================================================================
            IStories repository = new StoriesRepository(_context);

            List<Stories> stories = (List<Stories>)await repository.GetByProjectIdAsync(proyectId);
            List<StoriesGroupsPushes> storiespush = (List<StoriesGroupsPushes>)await repository.GetStorieGroupPushesByProjectIdAsync(proyectId);
            List<StoriesGroupsPulls> storiespull = (List<StoriesGroupsPulls>)await repository.GetStorieGroupPullsByProjectIdAsync(proyectId);

            //int[] storyIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] storyIds = stories.OrderBy(x=> x.IdStorie).Select(x => x.IdStorie).ToArray();

            //string[] storyNames = { "H1 (Ana)", "H2 (Carlos)", "H3 (Sofía)", "H4 (Pedro)", "H5 (Laura)", "H6 (Marcos)", "H7 (Isabel)", "H8 (Javier)", "H9 (David)", "H10 (Camila)" };
            string[] storyNames = stories.OrderBy(x => x.IdStorie).Select(x => x.TitleStorie + " - " + x.IdInter.InterName).ToArray();

            int x = storiespush.DistinctBy(x => x.Groups.IdGroup).Count() + storiespull.DistinctBy(x => x.Groups.IdGroup).Count();
            int y = storyIds.Length;

            int[,] matrix = new int[x, y];

            // Llenar la matriz con datos reales del proyecto
            int i = 0;
            int j = 0;

            foreach (StoriesGroupsPushes grp in storiespush.DistinctBy(x => x.Groups.IdGroup).OrderBy(x => x.Groups.IdGroup))
            {
                foreach (StoriesGroupsPushes gp in storiespush.Where(x => x.Groups.IdGroup == grp.Groups.IdGroup).OrderBy(x => x.Stories.IdStorie).ToList())
                {
                    matrix[i, j] = gp.ValorPush;
                    j++;
                }
                j=0;
                i++;
            }

            foreach (StoriesGroupsPulls grp in storiespull.DistinctBy(x => x.Groups.IdGroup).OrderBy(x => x.Groups.IdGroup))
            {
                foreach (StoriesGroupsPulls gp in storiespull.Where(x => x.Groups.IdGroup == grp.Groups.IdGroup).OrderBy(x => x.Stories.IdStorie).ToList())
                {
                    matrix[i, j] = gp.ValorPull;
                    j++;
                }
                j = 0;
                i++;
            }

            // ===================================================================
            // PASO 2: EJECUCIÓN DEL ALGORITMO NATIVO
            // ===================================================================

            // 1. Preparar la matriz para que el constructor de la clase nativa la reciba.
            double[][] transposedMatrix = TransposeAndConvertMatrix(matrix);

            // 2. Instanciar y Ejecutar el algoritmo.
            var clusterer = new WardNativeServices(transposedMatrix, storyIds);
            clusterer.PerformClustering();

            // 3. Obtener el resultado final
            List<List<int>> finalIds = clusterer.GetFinalClusters(numberOfClusters);

            // 4. Mapear los IDs a nuestro DTO para el reporte.
            List<FinalClusterGroup> finalGroups = MapFinalReport(finalIds, storyNames, storyIds);


            foreach (var group in finalGroups)
            {
                foreach (var storieId in group.StoryIds)
                {
                    await CreateStorieClustereAsync(proyectId, storieId, group.ClusterId);
                }
            }

            return finalGroups;
        }

        // --- FUNCIÓN DE SOPORTE: TRANSPONER Y CONVERTIR ---
        private static double[][] TransposeAndConvertMatrix(int[,] original)
        {
            int numRows = original.GetLength(0);
            int numCols = original.GetLength(1);

            double[][] transposed = new double[numCols][];

            for (int j = 0; j < numCols; j++)
            {
                transposed[j] = new double[numRows];
                for (int i = 0; i < numRows; i++)
                {
                    transposed[j][i] = (double)original[i, j];
                }
            }
            return transposed;
        }

        // --- FUNCIÓN DE SOPORTE: MAPEO DE RESULTADOS ---
        private static List<FinalClusterGroup> MapFinalReport(List<List<int>> finalIds, string[] storyNames, int[] storyIds)
        {
            var finalGroups = new List<FinalClusterGroup>();
            int clusterIdCounter = 1;

            foreach (var group in finalIds)
            {
                // Mapear los IDs a los nombres para el reporte final.
                List<string> groupNames = group.Select(id => storyNames[Array.IndexOf(storyIds, id)]).ToList();

                finalGroups.Add(new FinalClusterGroup
                {
                    ClusterId = clusterIdCounter++,
                    StoryIds = group,
                    StoryNames = groupNames
                });
            }
            return finalGroups;
        }

        public async Task<List<StoriesClusters>> GetStoriesClustersAsync(int proyectId)
        {
            var clusteres = await _context.StoriesClusters
                 .Include(x => x.Project)
                 .Include(x => x.Stories)
                 .Include(x => x.Stories.IdInter)
                 .Where(x => x.Project.IdProject == proyectId).AsQueryable().AsNoTracking().ToListAsync();
            return clusteres!;
        }

        public async Task<bool> CreateStorieClustereAsync(int proyectId, int storieId, int clustereId)
        {
            var storie = _context.Stories.Where(x => x.IdStorie == storieId).AsQueryable().AsNoTracking().FirstOrDefault();
            if (storie == null)
            {
                throw new InvalidOperationException("La historia no existe.");
            }

            var push = _context.Projects.Where(x => x.IdProject == proyectId).AsQueryable().AsNoTracking().FirstOrDefault();
            if (push == null)
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var result = await _context.Database.ExecuteSqlAsync(
                $"Insert into StoriesClusters (ProjectIdProject, StoriesIdStorie, IdCluster) Values( {proyectId}, {storieId}, {clustereId})");

            return true;
        }

        public async Task<bool> DeleteStorieClustereAsync(int proyectId)
        {
            var result = await _context.Database.ExecuteSqlAsync(
                $"Delete from StoriesClustersJobs Where ProjectIdProject = {proyectId}");

            var result2 = await _context.Database.ExecuteSqlAsync(
                $"Delete from StoriesClusters Where ProjectIdProject = {proyectId}");

            return true;
        }

        public async Task<List<StoriesClustersJobs>> GetClustersJobsAsync(int proyectId)
        {
            var clusteres = await _context.StoriesClustersJobs
                .Include(x => x.Project)
                .Where(x => x.Project.IdProject == proyectId).AsQueryable().AsNoTracking().ToListAsync();
            return clusteres!;
        }

        public async Task<List<StoriesClustersJobs>> GetClustersJobsByClusterAsync(int proyectId, int clusterid)
        {
            var clusteres = await _context.StoriesClustersJobs
                .Include(x => x.Project)
                .Where(x => x.Project.IdProject == proyectId && x.IdCluster == clusterid).AsQueryable().AsNoTracking().ToListAsync();
            return clusteres!;
        }

        public async Task<List<StoriesClustersJobs>> GetClustersJobsByTipoClusterAsync(int proyectId, int clusterid, int tipoid)
        {
            var clusteres = await _context.StoriesClustersJobs
                .Include(x => x.Project)
                .Where(x => x.Project.IdProject == proyectId && x.IdCluster == clusterid && x.IdTipo == tipoid).AsQueryable().AsNoTracking().ToListAsync();
            return clusteres!;
        }

        public async Task<bool> CreateClusterJobsAsync(CreateClustersJobs jobs)
        {
            StoriesClustersJobs auxJobs = new StoriesClustersJobs();
            auxJobs.IdTipo = jobs.IdTipo;
            auxJobs.Descripcion = jobs.Descripcion;
            auxJobs.IdCluster = jobs.IdCluster;

            var project = _context.Projects.Where(x => x.IdProject == jobs.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxJobs.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var cluster = _context.StoriesClusters.Where(x => x.Project.IdProject == jobs.IdProject && x.IdCluster == jobs.IdCluster).AsQueryable().AsNoTracking().FirstOrDefault();
            if (cluster == null)
            {
                throw new InvalidOperationException("El cluster no existe.");
            }

            await _context.StoriesClustersJobs.AddAsync(auxJobs);
            _context.Entry(auxJobs.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxJobs).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> UpdateClusterJobsAsync(CreateClustersJobs jobs)
        {
            StoriesClustersJobs auxJobs = new StoriesClustersJobs();
            auxJobs.Id = jobs.Id;
            auxJobs.IdTipo = jobs.IdTipo;
            auxJobs.Descripcion = jobs.Descripcion;
            auxJobs.IdCluster = jobs.IdCluster;

            var project = _context.Projects.Where(x => x.IdProject == jobs.IdProject).AsQueryable().AsNoTracking().FirstOrDefault();
            if (project != null)
            {
                auxJobs.Project = project;
            }
            else
            {
                throw new InvalidOperationException("El proyecto no existe.");
            }

            var cluster = _context.StoriesClusters.Where(x => x.Project.IdProject == jobs.IdProject && x.IdCluster == jobs.IdCluster).AsQueryable().AsNoTracking().FirstOrDefault();
            if (cluster == null)
            {
                throw new InvalidOperationException("El cluster no existe.");
            }

            _context.StoriesClustersJobs.Update(auxJobs);
            _context.Entry(auxJobs.Project).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            _context.Entry(auxJobs).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Detached;

            return true;
        }

        public async Task<bool> DeleteClusterJobsAsync(int id)
        {
            var entidad = await _context.StoriesClustersJobs.FindAsync(id);
            if (entidad != null)
            {
                _context.StoriesClustersJobs.Remove(entidad);
                await _context.SaveChangesAsync();
                _context.Entry(entidad).State = EntityState.Detached;
                return true;
            }
            return false;
        }
    }
}