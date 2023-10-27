using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ProductionPlanResponse
    {
        public Production_Plan Production_plan { get; set; } = new Production_Plan();
        public Product Product { get; set; } = new Product();
        public List<Production> listProductions { get; set; } = new List<Production>();
    }
}
