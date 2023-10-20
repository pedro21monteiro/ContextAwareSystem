using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonComponentProduct
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int ProductId { get; set; }
        public int Quantidade { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
