using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonSchedule
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int Shift { get; set; }
        public DateTime LastUpdate { get; set; }
        public int LineId { get; set; }
        public int? OperatorId { get; set; }
        public int? SupervisorId { get; set; }
    }
}
