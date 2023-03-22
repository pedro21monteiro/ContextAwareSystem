using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseLineInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro 
        public Line Line { get; set; } = new Line();
        public List<Stop> listStops{ get; set; } = new List<Stop>();
        public Coordinator Coordinator { get; set; } = new Coordinator();
        public Product Product { get; set; } = new Product();
        public List<Production> listProductions { get; set; } = new List<Production>();

    }
}
