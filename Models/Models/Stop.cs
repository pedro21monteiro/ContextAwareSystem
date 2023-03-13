using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public bool Planed { get; set; }

        public int LineId { get; set; }
        //Os date times vão ter por default ano 1, dia 1 , mes 1
        //Ex: DateTime dt3 = new DateTime(1, 1, 1, 5, 10, 20);
        public DateTime InitialDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public Reason? Reason { get; set; }
        public int? ReasonId { get; set; }
    }
}
