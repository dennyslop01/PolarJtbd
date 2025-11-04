namespace Jtbd.Domain.ViewModel
{
    public class ClusteringStep
    {
        // Índice del primer clúster unido
        public int ClusterAIndex { get; set; }
        // Índice del segundo clúster unido
        public int ClusterBIndex { get; set; }
        // Distancia a la que se unieron (altura del dendrograma)
        public double Distance { get; set; }
    }
}
