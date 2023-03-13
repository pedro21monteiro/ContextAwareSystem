using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Reference { get; set; } = String.Empty;
        public string Name { get; set; } =String.Empty;  
        public List<Component> listComponents { get; set; } = new List<Component>();
    }
}
