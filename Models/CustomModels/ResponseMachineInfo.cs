using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseMachineInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public string MachineName { get; set; } = String.Empty;
        public Line line { get; set; }
        public int LineId { get; set; }
        public List<Stop> listStops { get; set; } = new List<Stop>();

    }
}
