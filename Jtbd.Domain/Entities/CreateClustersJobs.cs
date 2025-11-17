using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Domain.Entities
{
    public class CreateClustersJobs
    {
        public int Id { get; set; }
        public int IdProject { get; set; }
        public int IdCluster { get; set; }
        public int IdTipo { get; set; }

        [Required(ErrorMessage = "Debe agregar una descripción.")]
        public string? Descripcion { get; set; }
    }
}
