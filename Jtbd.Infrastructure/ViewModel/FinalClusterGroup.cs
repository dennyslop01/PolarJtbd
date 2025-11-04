using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtbd.Infrastructure.ViewModel
{
    public class FinalClusterGroup
    {
        public int ClusterId { get; set; }
        public List<int> StoryIds { get; set; }
    }
}
