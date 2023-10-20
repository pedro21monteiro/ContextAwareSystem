using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonReason
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }

    }
}
