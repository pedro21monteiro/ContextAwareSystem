using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Line
    {
        public int Id { get; set; }
        public bool Priority { get; set; } //true-Urgente, false- não urgente     
        public int CoordenatorId { get; set; }
    }
}
