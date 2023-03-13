using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string IdFirebase { get; set; } = string.Empty;
        public int Type { get; set; }  //1-"wearable", 2-"tablet" ...etc
        public int LineId { get; set; }
    }
}
