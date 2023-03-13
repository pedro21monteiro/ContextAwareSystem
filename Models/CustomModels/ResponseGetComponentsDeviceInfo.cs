using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseGetComponentsDeviceInfo
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        public List<Component> listComponents { get; set; } = new List<Component>();
    }
}
