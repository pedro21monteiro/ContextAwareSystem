using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseSupervisorInfo 
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public Supervisor Supervisor { get; set; } = new Supervisor();
        public List<Line> listLine { get; set; } = new List<Line>();
        public List<Schedule_Worker_Line> listSWL { get; set; } = new List<Schedule_Worker_Line>();
    }
}
