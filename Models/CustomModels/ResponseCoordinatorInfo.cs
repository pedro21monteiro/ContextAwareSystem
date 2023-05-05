using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseCoordinatorInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public Coordinator Coordinator { get; set; } = new Coordinator();
        public List<Line> listLine { get; set; } = new List<Line>();

    }
}
