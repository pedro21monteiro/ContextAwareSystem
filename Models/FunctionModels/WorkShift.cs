using Models.Models;
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


        
        //ver se production se encontra nesse workshift
        public bool IsProductionInWorkshift(Production p)
        {
            //se forem ambas as datas da p menores que a data inicial do workshift não pertence
            if ((p.InitialDate.CompareTo(this.InitialDate)<0 && p.EndDate.CompareTo(this.InitialDate) < 0) || (p.InitialDate.CompareTo(this.EndDate) > 0 && p.EndDate.CompareTo(this.EndDate) > 0))
            {
                return true;
            }
            return false;
        }
    }
}
