using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FunctionModels
{
    public class AlertStopRequest
    {
        public int lineId { get; set; }
        public int shift { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public string message { get; set; } = string.Empty;

    }
}
