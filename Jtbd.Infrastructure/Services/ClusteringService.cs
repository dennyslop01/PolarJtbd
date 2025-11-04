using Aglomera;
using SimMetrics.Net.Metric;
using Jtbd.Infrastructure.ViewModel;


namespace Jtbd.Infrastructure.Services
{
    public class ClusteringService
    {
        public List<DataPoint> PrepareDataForClustering(int[][] matrix, int[] storyIds)
        {
            var dataPoints = new List<DataPoint>();

            // Iteramos por cada fila de la matriz (cada historia)
            for (int i = 0; i < matrix.Length; i++)
            {
                // Convertimos la fila de int[] a double[]
                double[] storyVector = matrix[i].Select(val => (double)val).ToArray();

                // Creamos el DataPoint. Le pasamos el vector y un identificador único.
                // Es CRUCIAL guardar el ID de la historia aquí para saber a quién pertenece cada punto.
                dataPoints.Add(new DataPoint(storyVector, storyIds[i].ToString()));
            }
            return dataPoints;
        }

        public AgglomerativeClusteringAlgorithm<DataPoint> ConfigureAlgorithm()
        {
            // 1. Creamos una instancia de la métrica de distancia que queremos usar.
            var distance = new EuclideanDistance();

            // 2. Creamos una instancia del método de vinculación, pasándole la métrica de distancia.
            //    Esta es la implementación específica del Método de Ward.
            var linkage = new WardLinkage<DataPoint>(distance);


            // 3. Creamos la instancia principal del algoritmo de clustering.
            var algorithm = new AgglomerativeClusteringAlgorithm<DataPoint>(linkage);

            return algorithm;
        }

        public ClusteringResult<DataPoint> PerformClustering(AgglomerativeClusteringAlgorithm<DataPoint> algorithm, ClusterSet<DataPoint> dataPoints)
        {
            // Ejecutamos el algoritmo. Esta llamada hace todo el trabajo pesado.
            return algorithm.GetClustering(dataPoints);
        }

        public List<FinalClusterGroup> GetFinalClusters(ClusteringResult<DataPoint> result, int numberOfClusters)
        {
            // "Cortamos" el árbol en el número de clústeres solicitado por el usuario.
            // Esta es una función nativa de Aglomera, muy eficiente.
            var clusters = result.GetClusters(numberOfClusters);

            var finalGroups = new List<FinalClusterGroup>();
            int clusterIdCounter = 1;
            foreach (var cluster in clusters)
            {
                finalGroups.Add(new FinalClusterGroup
                {
                    ClusterId = clusterIdCounter++,
                    // Para cada clúster, obtenemos la lista de IDs de las historias que lo componen.
                    StoryIds = cluster.Select(dataPoint => int.Parse(dataPoint.Id)).ToList()
                });
            }
            return finalGroups;
        }
    }
}
