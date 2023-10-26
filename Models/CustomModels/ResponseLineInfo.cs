using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseLineInfo
    {
        public string Message { get; set; } = String.Empty;
        public Line Line { get; set; } = new Line();
        public Coordinator Coordinator { get; set; } = new Coordinator();
        public Worker Worker { get; set; } = new Worker();
        public List<Stop> listStops{ get; set; } = new List<Stop>();
        public List<ResponseProductionPlan> listProductionsByProductionPlan { get; set; } = new List<ResponseProductionPlan>();

    }
}
