using Jtbd.Application.Interfaces;
using Jtbd.Domain.Entities;
using Jtbd.Domain.ViewModel;
using Jtbd.Infrastructure.DataContext;
using Jtbd.Infrastructure.Services;

namespace Jtbd.Infrastructure.Repositories
{
    public class MatrizWardRepository(JtbdDbContext context) : IMatrizWard
    {
        private readonly JtbdDbContext _context = context;

        public async Task<List<FinalClusterGroup>> GetMatrizWardAsync(int proyectId, int numberOfClusters)
        {
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
            foreach (Stories st in stories.OrderBy(x => x.IdStorie))
            {
                foreach(StoriesGroupsPushes gp in storiespush.Where(x => x.Stories.IdStorie == st.IdStorie).OrderBy(x => x.Groups.IdGroup).ToList())
                {
                    matrix[i, j] = gp.ValorPush;
                    j++;
                }

                foreach (StoriesGroupsPulls gp in storiespull.Where(x => x.Stories.IdStorie == st.IdStorie).OrderBy(x => x.Groups.IdGroup).ToList())
                {
                    matrix[i, j] = gp.ValorPull;
                    j++;
                }
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
    }
}