using System;
using Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseOperatorInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro

        //já não são necessarias, ver com o rodrigo
        public Operator Operator { get; set; } = new Operator();

        public List<Line> listLine { get; set; } = new List<Line>();
        public List<Schedule_Worker_Line> listSWL { get; set; } = new List<Schedule_Worker_Line>();
      
    }
}
