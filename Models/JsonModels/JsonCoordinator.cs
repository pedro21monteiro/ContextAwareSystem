using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonCoordinator
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
