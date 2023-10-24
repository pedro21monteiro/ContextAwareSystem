using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ContextModels;

namespace Models.CustomModels
{
    public class ResponseOperatorInfo
    {
        public string Message { get; set; } = String.Empty;
        public Operator Operator { get; set; } = new Operator();
        public Worker Worker { get; set; } = new Worker();
        public List<Line> listLine { get; set; } = new List<Line>();
        public List<Schedule_Worker_Line> listSWL { get; set; } = new List<Schedule_Worker_Line>();
      
    }
}
