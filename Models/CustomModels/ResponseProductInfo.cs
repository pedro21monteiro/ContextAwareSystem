using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ContextModels;

namespace Models.CustomModels
{
    public class ResponseProductInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public Product Product { get; set; } = new Product();

    }
}
