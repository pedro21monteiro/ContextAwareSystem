using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ContextModels;

namespace Models.CustomModels
{
    public class ResponseStopsInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public List<StopResponse> listNewStops { get; set; } = new List<StopResponse>();
    }
}
