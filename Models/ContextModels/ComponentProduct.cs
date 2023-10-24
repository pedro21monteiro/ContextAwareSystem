using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.ContextModels
{
    public class ComponentProduct
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int ProductId { get; set; }
        public int Quantidade { get; set; }
    }
}
