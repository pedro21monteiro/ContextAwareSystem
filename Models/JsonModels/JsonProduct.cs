using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string LabelReference { get; set; } = String.Empty;
        public TimeSpan Cycle { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
