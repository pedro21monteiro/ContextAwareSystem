using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonStop
    {
        public int Id { get; set; }
        public bool Planned { get; set; }
        public DateTime InitialDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; }
        public int Shift { get; set; }
        public DateTime LastUpdate { get; set; }
        public int LineId { get; set; }
        public int? ReasonId { get; set; }
    }
}
