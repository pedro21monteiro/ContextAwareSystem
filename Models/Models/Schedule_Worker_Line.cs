using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Schedule_Worker_Line
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int LineId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
