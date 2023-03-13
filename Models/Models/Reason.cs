using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Reason
    {
        public int Id { get; set; }
        public int StopId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? MachineId { get; set; }
    }
}
