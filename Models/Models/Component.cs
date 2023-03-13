using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Reference { get; set; } = String.Empty;
        public int Category { get; set; }//0 - Sem categoria, 1-Etiqueta , 2- parafusos / etc...
    }
}
