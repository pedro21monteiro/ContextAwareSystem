using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonProductionPlan
    {
        public int Id { get; set; }
        public int Goal { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Shift { get; set; }
        public DateTime LastUpdate { get; set; }
        public int ProductId { get; set; }
        public int LineId { get; set; }
    }
}
