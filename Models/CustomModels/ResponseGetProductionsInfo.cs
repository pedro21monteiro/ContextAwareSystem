using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseGetProductionsInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro    
        public List<Production> listProductions { get; set; } = new List<Production>();
    }
}
