using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FunctionModels
{
    public class SendAlertRequest
    {
        public string Message { get; set; } = string.Empty;
        public Stop? Stop { get; set; }
        public Production? Production { get; set; }
        public MissingComponent? MissingComponent { get; set; }

    }
}
