using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FunctionModels
{
    public class WorkShift
    {
        public int Shift { get; set; }
        public string ShiftString { get; set; } = string.Empty;
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }    

    }
}
