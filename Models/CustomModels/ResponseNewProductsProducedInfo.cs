using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseNewProductsProducedInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public int LastProduced { get; set; }
        public int NewProduced { get; set; }
        public int AtualProduced { get; set; }
        public DateTime LastCheck { get; set; }
        public DateTime AtualCheck { get; set; }
        
    }
}
