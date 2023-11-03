using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseGetMissingComponents
    {
        public string Message { get; set; } = String.Empty;
        public List<MissingComponentResponse> listMissingComponentes { get; set; } = new List<MissingComponentResponse>();
    }
}
