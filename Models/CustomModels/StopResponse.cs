using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class StopResponse
    {
        public Stop Stop { get; set; } = new Stop();
        public string Description { get; set; } = string.Empty;
    }
}
