using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Production
    {
        public int Id { get; set; }
        public int Objective { get; set; }
        public int Produced { get; set; }
        public string Order { get; set; } = string.Empty;
        public int LineId { get; set; }
        public int ProductId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; } 

    }
}
