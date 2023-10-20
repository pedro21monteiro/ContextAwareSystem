using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonDevice
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int LineId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
