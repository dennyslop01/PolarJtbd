using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Domain.ViewModel;
using Jtbd.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Jtbd.Infrastructure.Repositories
{
    public class MatrizWardRepository(JtbdDbContext context) : IMatrizWard
    {
        private readonly JtbdDbContext _context = context;

        public async Task<bool> GetMatrizWardAsync(int proyectId, int numberOfClusters)
        {
            await DeleteStorieClustereAsync(proyectId);

            // ===================================================================
            // PASO 1: DATOS DE ENTRADA (Matriz del proyecto)
            // ===================================================================
            IStories repository = new StoriesRepository(_context);

            List<StoriesGroupsPushes> storiespush = (List<StoriesGroupsPushes>)await repository.GetStorieGroupPushesByProjectIdAsync(proyectId);
            List<StoriesGroupsPulls> storiespull = (List<StoriesGroupsPulls>)await repository.GetStorieGroupPullsByProjectIdAsync(proyectId);
            List<DataPoint> data = new List<DataPoint>();

            int i = 0;
            foreach (StoriesGroupsPushes grp in storiespush.DistinctBy(x => x.Stories.IdStorie).OrderBy(x => x.Stories.IdStorie))
            {
                DataPoint auxiliar = new DataPoint();

                decimal[] feacture = new decimal[storiespush.Select(x => x.Groups.IdGroup).Distinct().Count() + storiespull.Select(x => x.Groups.IdGroup).Distinct().Count()];

                int j = 0;
                foreach (StoriesGroupsPushes gp in storiespush.Where(x => x.Stories.IdStorie == grp.Stories.IdStorie).OrderBy(x => x.Groups.IdGroup).ToList())
                {
                    feacture[j] = (decimal)gp.ValorPush;
                    j++;
                }

                foreach (StoriesGroupsPulls gp in storiespull.Where(x => x.Stories.IdStorie == grp.Stories.IdStorie).OrderBy(x => x.Groups.IdGroup).ToList())
                {
                    feacture[j] = (decimal)gp.ValorPull;
                    j++;
                }

                auxiliar.Index = i;
                auxiliar.Id = $"{grp.Stories.IdInter.InterName}/{grp.Stories.TitleStorie}";
                auxiliar.Features = feacture;
                data.Add(auxiliar);
                i++;
            }


            // ================================================================
            // OPTIMIZACIÓN: "Build Once, Cut Many"
            // ================================================================

            // A. Construimos el árbol completo UNA sola vez.
            // Esto es lo costoso computacionalmente (O(N^3)).
            var rootNode = BuildWardTree(data);

            // B. Realizamos los cortes (Clusterización)
            // Esto es instantáneo porque el árbol ya existe en memoria.
            var clusters = CutTree(rootNode, numberOfClusters);
            List<Stories> stories = (List<Stories>)await repository.GetByProjectIdAsync(proyectId);

            foreach (var group in clusters)
            {
                int idstorie = stories[group.Key].IdStorie;
                await CreateStorieClustereAsync(proyectId, idstorie, group.Value);
            }

            return true;
        }

        // Recibe 'List<DataPoint>' que puede ser nula, por eso el '?' en la firma no es estrictamente necesario si controlamos dentro, 
        // pero para evitar warnings en la llamada, lo dejamos limpio.
        public static Dictionary<int, int> ComputeClusters(List<DataPoint> data, int k)
        {
            if (data == null || data.Count == 0) return new Dictionary<int, int>();

            var rootNode = BuildWardTree(data);
            return CutTree(rootNode, k);
        }

        public static ClusterNode BuildWardTree(List<DataPoint> points)
        {
            var activeClusters = points.Select(p => new ClusterNode(p)).ToList();

            while (activeClusters.Count > 1)
            {
                decimal minIncrease = decimal.MaxValue;

                // CORRECCIÓN 1: Variables anulables explícitas
                ClusterNode? bestA = null;
                ClusterNode? bestB = null;

                for (int i = 0; i < activeClusters.Count; i++)
                {
                    for (int j = i + 1; j < activeClusters.Count; j++)
                    {
                        decimal increase = CalculateWardIncrease(activeClusters[i], activeClusters[j]);

                        if (increase < minIncrease)
                        {
                            minIncrease = increase;
                            bestA = activeClusters[i];
                            bestB = activeClusters[j];
                        }
                        else if (increase == minIncrease)
                        {
                            // Al acceder a propiedades de un nullable, el compilador puede quejarse.
                            // Aquí validamos que bestA/bestB no sean null antes de usarlos en la lógica de empate.
                            if (bestA != null && bestB != null)
                            {
                                int currentMinId = Math.Min(activeClusters[i].GetMinOriginalIndex(), activeClusters[j].GetMinOriginalIndex());
                                int bestMinId = Math.Min(bestA.GetMinOriginalIndex(), bestB.GetMinOriginalIndex());

                                if (currentMinId < bestMinId)
                                {
                                    bestA = activeClusters[i];
                                    bestB = activeClusters[j];
                                }
                            }
                            else
                            {
                                // Si es el primer par encontrado (bestA es null), lo tomamos.
                                bestA = activeClusters[i];
                                bestB = activeClusters[j];
                            }
                        }
                    }
                }

                // Si no se encontró par (imposible en lógica Ward, pero necesario para el compilador)
                if (bestA == null || bestB == null) break;

                var merged = new ClusterNode(bestA, bestB);
                activeClusters.Remove(bestA);
                activeClusters.Remove(bestB);
                activeClusters.Add(merged);
            }

            return activeClusters.First();
        }

        private static decimal CalculateWardIncrease(ClusterNode a, ClusterNode b)
        {
            decimal distSq = 0m;
            int dims = Math.Min(a.Centroid.Length, b.Centroid.Length);

            for (int k = 0; k < dims; k++)
            {
                decimal d = a.Centroid[k] - b.Centroid[k];
                distSq += d * d;
            }

            decimal factor = (decimal)(a.Count * b.Count) / (decimal)(a.Count + b.Count);
            return factor * distSq;
        }

        public static Dictionary<int, int> CutTree(ClusterNode root, int k)
        {
            var finalNodes = new List<ClusterNode> { root };

            while (finalNodes.Count < k)
            {
                // CORRECCIÓN 2: 'nodeToSplit' puede ser nulo si no se encuentra candidato
                ClusterNode? nodeToSplit = finalNodes
                    .Where(n => n.Left != null && n.Right != null)
                    .OrderByDescending(n => n.MergeCost)
                    .ThenBy(n => n.GetMinOriginalIndex())
                    .FirstOrDefault();

                if (nodeToSplit == null) break;

                finalNodes.Remove(nodeToSplit);

                // Como ya validamos en el Where que Left/Right no son null, usamos el operador ! ("sé que no es nulo")
                if (nodeToSplit.Left != null) finalNodes.Add(nodeToSplit.Left);
                if (nodeToSplit.Right != null) finalNodes.Add(nodeToSplit.Right);
            }

            var assignment = new Dictionary<int, int>();
            int label = 1;

            foreach (var node in finalNodes.OrderBy(n => n.GetMinOriginalIndex()))
            {
                var indices = node.GetAllIndices();
                foreach (var idx in indices) assignment[idx] = label;
                label++;
            }
            return assignment;
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