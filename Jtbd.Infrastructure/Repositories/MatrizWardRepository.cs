using Jtbd.Application.Interfaces;
using Jtbd.Domain.ViewModel;
using Jtbd.Infrastructure.Services;

namespace Jtbd.Infrastructure.Repositories
{
    public class MatrizWardRepository : IMatrizWard
    {
        public async Task<List<FinalClusterGroup>> GetMatrizWardAsync(int proyectId, int numberOfClusters)
        {
            // ===================================================================
            // PASO 1: DATOS DE ENTRADA (Matriz del proyecto)
            // ===================================================================
            int[] storyIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] storyNames = { "H1 (Ana)", "H2 (Carlos)", "H3 (Sofía)", "H4 (Pedro)", "H5 (Laura)", "H6 (Marcos)", "H7 (Isabel)", "H8 (Javier)", "H9 (David)", "H10 (Camila)" };
            int[,] matrix = new int[20, 10]
            {
                // H1 H2 H3 H4 H5 H6 H7 H8 H9 H10   <-- Historias
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // Push G1: Tiempo
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0}, // Push G2: Emoción
                {0, 0, 1, 0, 0, 0, 0, 1, 0, 0}, // Push G3: Salud
                {0, 0, 0, 1, 1, 0, 0, 0, 0, 0}, // Push G4: Presupuesto
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // Push G5: Facilidad
                {0, 0, 0, 0, 0, 0, 0, 0, 1, 0}, // Push G6: 'Picky Eaters'
                {0, 0, 0, 0, 0, 0, 0, 1, 0, 0}, // Push G7: Energía
                {0, 0, 0, 0, 0, 1, 0, 0, 0, 0}, // Push G8: Impresionar
                {0, 0, 0, 0, 1, 0, 0, 0, 0, 0}, // Push G9: Escasez
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, // Push G10: Costo Negocio
                {1, 0, 0, 1, 0, 0, 0, 0, 1, 0}, // Pull G1: Confianza
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0}, // Pull G2: Identidad
                {0, 0, 1, 0, 0, 1, 0, 0, 0, 1}, // Pull G3: Versatilidad
                {0, 0, 0, 1, 1, 0, 0, 0, 0, 1}, // Pull G4: Rendimiento
                {0, 0, 1, 0, 0, 0, 0, 1, 0, 0}, // Pull G5: Nutrición
                {1, 1, 0, 1, 1, 1, 1, 1, 1, 1}, // Pull G6: Sabor
                {0, 0, 0, 1, 0, 1, 0, 0, 0, 1}, // Pull G7: Socialización
                {0, 1, 1, 0, 0, 1, 0, 1, 0, 0}, // Pull G8: Logro
                {0, 0, 0, 0, 1, 0, 1, 0, 0, 1}, // Pull G9: Básico
                {0, 0, 0, 0, 0, 1, 0, 0, 0, 0}  // Pull G10: Gourmet
            };

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
