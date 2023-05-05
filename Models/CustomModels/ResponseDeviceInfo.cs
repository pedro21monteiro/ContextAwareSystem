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
        public string Message { get; set; } = String.Empty;//Mensagem de erro    
        //Operaador ou Coordenador
        public string Type { get; set; } = String.Empty;
        //----------------Se for Operador-----------
        public Line line { get; set; } = new Line();
        public string ProductName { get; set; } = String.Empty;
        public string TagReference { get; set; } = String.Empty;
        //neste momento pode retornar todos
        public List<Component> listMissingComponentes { get; set; } = new List<Component>();
        public int WorkShift { get; set; }
        public string WorkShiftString { get; set; } = String.Empty;

        //Se for Coordenador
        public Coordinator Coordinator { get; set; } = new Coordinator();
        

    }
}
