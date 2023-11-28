using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class MissingComponentResponse
    {
        public int Id { get; set; } 
        public Line Line{ get; set; } = new Line();
        public Component Component{ get; set; } = new Component();
        public DateTime OrderDate { get; set; }
    }
}
