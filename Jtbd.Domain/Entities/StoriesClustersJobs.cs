using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class StoriesClustersJobs
    {
        public int Id { get; set; }
        public Projects? Project { get; set; }
        public int IdCluster { get; set; }
        public int IdTipo { get; set; }
        public string? Descripcion { get; set; }
    }
}
