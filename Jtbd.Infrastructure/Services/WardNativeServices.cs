using Jtbd.Domain.ViewModel;

namespace Jtbd.Infrastructure.Services
{
    public class WardNativeServices
    {
        // Matriz de datos de entrada (cada fila es una historia/punto).
        private readonly double[][] _data;
        // IDs originales de las historias, en el mismo orden que las filas de _data.
        private readonly int[] _originalIds;

        // Lista que almacena cada paso de unión del algoritmo.
        private readonly List<ClusteringStep> _history = new List<ClusteringStep>();

        // Lista dinámica de clústeres que se van uniendo. Inicializada para evitar warnings.
        private List<Cluster> _clusters = new List<Cluster>();

        public WardNativeServices(double[][] data, int[] originalIds)
        {
            if (data == null || originalIds == null || data.Length != originalIds.Length)
            {
                throw new ArgumentException("Los datos y los IDs deben tener el mismo tamaño y no pueden ser nulos.");
            }
            _data = data;
            _originalIds = originalIds;
        }

        // --- Algoritmo de Ward ---

        /// <summary>
        /// Ejecuta el algoritmo de clustering jerárquico de Ward.
        /// Construye el árbol de uniones y guarda el resultado en _history.
        /// </summary>
        public void PerformClustering()
        {
            int n = _data.Length;

            // 1. Inicialización: Cada historia es un clúster de tamaño 1.
            _clusters = new List<Cluster>(n);
            for (int i = 0; i < n; i++)
            {
                _clusters.Add(new Cluster
                {
                    Centroid = _data[i],
                    Size = 1,
                    OriginalIds = new List<int> { _originalIds[i] }
                });
            }

            // 2. Cálculo inicial de la matriz de distancias.
            // Distancia inicial es la Euclidiana al cuadrado entre los puntos.
            double[,] distances = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    distances[i, j] = distances[j, i] = EuclideanDistanceSquared(_clusters[i].Centroid, _clusters[j].Centroid);
                }
            }

            // 3. Proceso Aglomerativo: Unir clústeres hasta que quede solo uno.
            while (_clusters.Count > 1)
            {
                // 3a. Encontrar la unión de menor distancia (la mejor fusión).
                double minDistance = double.MaxValue;
                int mergeIndexA = -1, mergeIndexB = -1;

                for (int i = 0; i < _clusters.Count; i++)
                {
                    for (int j = i + 1; j < _clusters.Count; j++)
                    {
                        if (distances[i, j] < minDistance)
                        {
                            minDistance = distances[i, j];
                            mergeIndexA = i;
                            mergeIndexB = j;
                        }
                    }
                }

                // Si no se encontró la fusión, algo está mal (debería ser la última iteración).
                if (mergeIndexA == -1) break;

                // 3b. Guardar el paso de unión en la historia.
                _history.Add(new ClusteringStep
                {
                    ClusterAIndex = mergeIndexA,
                    ClusterBIndex = mergeIndexB,
                    Distance = Math.Sqrt(minDistance) // Guardamos la distancia real (no al cuadrado)
                });

                // 3c. Crear el nuevo clúster unido (Cluster C).
                Cluster clusterA = _clusters[mergeIndexA];
                Cluster clusterB = _clusters[mergeIndexB];

                // El nuevo centroide se calcula como el promedio ponderado (según el método de Ward).
                double[] newCentroid = new double[clusterA.Centroid.Length];
                for (int k = 0; k < newCentroid.Length; k++)
                {
                    newCentroid[k] = (clusterA.Centroid[k] * clusterA.Size + clusterB.Centroid[k] * clusterB.Size) / (clusterA.Size + clusterB.Size);
                }

                Cluster newCluster = new Cluster
                {
                    Centroid = newCentroid,
                    Size = clusterA.Size + clusterB.Size,
                    OriginalIds = clusterA.OriginalIds.Concat(clusterB.OriginalIds).ToList()
                };

                // 3d. Actualizar la lista de clústeres y la matriz de distancias.

                // Primero: Calcular la distancia del nuevo clúster (C) a todos los demás (D).
                // La fórmula de Ward calcula la distancia D(C, D)
                for (int i = 0; i < _clusters.Count; i++)
                {
                    if (i == mergeIndexA || i == mergeIndexB) continue;

                    double distCD = DistanceWardLinkage(
                        distances[mergeIndexA, i], distances[mergeIndexB, i],
                        distances[mergeIndexA, mergeIndexB],
                        clusterA.Size, clusterB.Size, _clusters[i].Size
                    );

                    distances[i, mergeIndexA] = distances[mergeIndexA, i] = distCD;
                }

                // Segundo: Reemplazar el clúster A por el nuevo clúster C, y eliminar el B.
                _clusters[mergeIndexA] = newCluster;
                _clusters.RemoveAt(mergeIndexB);

                // Tercero: Ajustar la matriz de distancias para reflejar la eliminación de B.
                // Movemos las filas/columnas para eliminar la entrada del clúster B.
                // Esta es la parte más compleja del código nativo.
                for (int i = mergeIndexB; i < _clusters.Count; i++)
                {
                    for (int j = 0; j < _clusters.Count + 1; j++)
                    {
                        // Mover los valores de distancia hacia arriba/izquierda.
                        distances[i, j] = distances[i + 1, j];
                        distances[j, i] = distances[j, i + 1];
                    }
                }
            }
        }

        // --- Funciones de Utilidad Matemática ---

        /// <summary>
        /// Calcula la distancia Euclidiana al cuadrado entre dos vectores.
        /// </summary>
        private static double EuclideanDistanceSquared(double[] vectorA, double[] vectorB)
        {
            double sumOfSquares = 0;
            for (int i = 0; i < vectorA.Length; i++)
            {
                sumOfSquares += Math.Pow(vectorA[i] - vectorB[i], 2);
            }
            return sumOfSquares;
        }

        /// <summary>
        /// Calcula la nueva distancia D(C, D) usando el método de Ward (Actualización de la Distancia).
        /// Esta es la fórmula de aglomeración recurrente para Ward.
        /// </summary>
        private static double DistanceWardLinkage(double distAD, double distBD, double distAB, int sizeA, int sizeB, int sizeD)
        {
            int sizeC = sizeA + sizeB;

            // La distancia aquí está en términos de distancias al cuadrado (varianza).
            return (sizeA + sizeD) * distAD / sizeC
                 + (sizeB + sizeD) * distBD / sizeC
                 - sizeD * distAB / sizeC;
        }

        // --- Funcionalidad de Reporte (El Corte del Árbol) ---

        /// <summary>
        /// Obtiene la lista final de clústeres cortando el árbol según el K deseado.
        /// </summary>
        /// <param name="k">Número de clústeres deseado por el analista.</param>
        /// <returns>Lista de listas, donde cada lista interna contiene los IDs de las historias.</returns>
        public List<List<int>> GetFinalClusters(int k)
        {
            if (k <= 0 || k > _data.Length)
            {
                // Valor seguro por defecto si K es inválido.
                k = 1;
            }

            // Si K es 1, devolvemos el único clúster que queda al final.
            if (k == 1)
            {
                // Como solo queda un clúster en _clusters, devolvemos su contenido.
                return new List<List<int>> { _clusters[0].OriginalIds };
            }

            // Usamos la historia de uniones para "deshacer" las últimas (N-K) uniones.
            int n = _data.Length;
            int mergesToUndo = n - k;

            // Si el número de clústeres es mayor o igual al número de puntos (o K muy pequeño), 
            // devolvemos los clústeres actuales (que deben ser los puntos individuales si mergesToUndo <= 0).
            if (mergesToUndo <= 0)
            {
                // Reconstruir la lista de clusters originales si la lista _clusters se vació
                // al terminar el PerformClustering. 
                // Para fines de esta implementación nativa que deja 1 clúster al final,
                // necesitamos recrear la lista inicial si K > 1.

                // Nota: Este path solo se toma si K es igual a N (num. de historias)
                List<Cluster> initialClusters = new List<Cluster>(n);
                for (int i = 0; i < n; i++)
                {
                    initialClusters.Add(new Cluster
                    {
                        OriginalIds = new List<int> { _originalIds[i] }
                    });
                }
                return initialClusters.Select(c => c.OriginalIds).ToList();
            }

            // --- Reconstrucción del Árbol hasta K ---

            // 1. Iniciar con cada punto como un clúster individual (la base del dendrograma).
            List<Cluster> currentClusters = new List<Cluster>(n);
            for (int i = 0; i < n; i++)
            {
                currentClusters.Add(new Cluster
                {
                    // No necesitamos Centroid ni Size, solo el ID original.
                    OriginalIds = new List<int> { _originalIds[i] }
                });
            }

            // 2. Recorrer la historia y ejecutar las primeras 'mergesToUndo' uniones.
            for (int step = 0; step < mergesToUndo; step++)
            {
                ClusteringStep merge = _history[step];

                // Los índices de la fusión (ClusterAIndex y ClusterBIndex) apuntan a 
                // la lista currentClusters en ese momento.

                Cluster clusterA = currentClusters[merge.ClusterAIndex];
                Cluster clusterB = currentClusters[merge.ClusterBIndex];

                // Crear el clúster unido
                Cluster newCluster = new Cluster
                {
                    OriginalIds = clusterA.OriginalIds.Concat(clusterB.OriginalIds).ToList()
                };

                // Reemplazar A por el nuevo clúster y eliminar B.
                // NOTA: El orden de eliminación es CRUCIAL. Debemos eliminar el índice mayor primero.
                int indexA = merge.ClusterAIndex;
                int indexB = merge.ClusterBIndex;

                // El algoritmo de Ward usa los índices de la lista original, pero
                // para la reconstrucción, debemos ordenar los índices para no fallar.
                int indexToRemove = Math.Max(indexA, indexB);
                int indexToKeep = Math.Min(indexA, indexB);

                // Sobrescribir el que queda y eliminar el otro
                currentClusters[indexToKeep] = newCluster;
                currentClusters.RemoveAt(indexToRemove);
            }

            // 3. El resultado es la lista de los K clústeres.
            return currentClusters.Select(c => c.OriginalIds).ToList();
        }
    }
}
