using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonComponent
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Reference { get; set; } = String.Empty;
        public int Category { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
