using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonLine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Priority { get; set; }
        public DateTime LastUpdate { get; set; }
        public int CoordinatorId { get; set; }
    }
}
