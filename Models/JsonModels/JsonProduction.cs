using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonProduction
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public DateTime Day { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Production_PlanId { get; set; }
    }
}
