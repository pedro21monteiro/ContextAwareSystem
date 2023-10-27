using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseDeviceInfo
    {
        public string Message { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;

        //Se for Operador
        public Line Line { get; set; } = new Line();
        public string ProductName { get; set; } = String.Empty;
        public string TagReference { get; set; } = String.Empty;
        public List<Component> listMissingComponentes { get; set; } = new List<Component>();
        public int WorkShift { get; set; }

        //Se for Coordenador
        public Coordinator Coordinator { get; set; } = new Coordinator();
        public Worker Worker { get; set; } = new Worker();
        public List<Line> listResponsavelLines { get; set; } = new List<Line>();
    }
}
