using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ContextModels
{
    public class ItensToUpdate
    {
    
        public List<Production> productions { get; set; } = new List<Production>();
        public List<Stop> stops { get; set; } = new List<Stop>();

    }
}
