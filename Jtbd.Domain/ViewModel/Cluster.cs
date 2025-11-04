namespace Jtbd.Domain.ViewModel
{
    public class Cluster
    {
        // Vector que representa el centroide del clúster (promedio de los puntos).
        // Se inicializa para evitar warnings CS8618.
        public double[] Centroid { get; set; } = Array.Empty<double>();
        // Número de puntos (historias) que contiene el clúster.
        public int Size { get; set; }
        // Lista de IDs de las historias originales que contiene.
        public List<int> OriginalIds { get; set; } = new List<int>();
    }
}
