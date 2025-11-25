namespace Jtbd.Domain.ViewModel
{
    // ==========================================
    //  CLASES DE DATOS
    // ==========================================
    public class DataPoint
    {
        public int Index { get; set; }
        // CORRECCIÓN 3: String requerido o inicializado. Usamos "string.Empty" para evitar nulls.
        public string Id { get; set; } = string.Empty;
        public decimal[] Features { get; set; } = Array.Empty<decimal>();
    }

    public class ClusterNode
    {
        // CORRECCIÓN 4: Hijos pueden ser nulos (hojas del árbol)
        public ClusterNode? Left { get; set; }
        public ClusterNode? Right { get; set; }

        public decimal[] Centroid { get; set; }
        public int Count { get; set; }
        public decimal MergeCost { get; set; }

        private List<int>? _cachedIndices;

        public ClusterNode(DataPoint p)
        {
            Centroid = p.Features.ToArray();
            Count = 1;
            MergeCost = 0m;
            _cachedIndices = new List<int> { p.Index };
            // Left y Right quedan null por defecto, lo cual es correcto para una hoja
        }

        public ClusterNode(ClusterNode left, ClusterNode right)
        {
            Left = left;
            Right = right;
            Count = left.Count + right.Count;

            Centroid = new decimal[left.Centroid.Length];
            for (int i = 0; i < Centroid.Length; i++)
                Centroid[i] = (left.Centroid[i] * left.Count + right.Centroid[i] * right.Count) / Count;

            decimal distSq = 0m;
            for (int i = 0; i < Centroid.Length; i++)
            {
                decimal d = left.Centroid[i] - right.Centroid[i];
                distSq += d * d;
            }
            MergeCost = (decimal)(left.Count * right.Count) / (decimal)(left.Count + right.Count) * distSq;
        }

        public List<int> GetAllIndices()
        {
            if (_cachedIndices != null) return _cachedIndices;

            var list = new List<int>();
            if (Left != null) list.AddRange(Left.GetAllIndices());
            if (Right != null) list.AddRange(Right.GetAllIndices());

            _cachedIndices = list;
            return list;
        }

        public int GetMinOriginalIndex()
        {
            // Si la lista está vacía (no debería), devolvemos un valor alto seguro
            var indices = GetAllIndices();
            return indices.Count > 0 ? indices.Min() : int.MaxValue;
        }
    }
}
