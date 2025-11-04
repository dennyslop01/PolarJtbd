using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.ViewModel
{
    public class DendrogramNode
    {
        public List<int> Members { get; set; } // IDs de las historias en este clúster
        public double Distance { get; set; }    // Altura a la que se une
        public DendrogramNode LeftChild { get; set; }
        public DendrogramNode RightChild { get; set; }
    }

}
