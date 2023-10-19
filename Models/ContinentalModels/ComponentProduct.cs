using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.ContinentalModels
{
    public class ComponentProduct
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Component Component { get; set; } = new Component();
        public int ComponentId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Product Product { get; set; } = new Product();
        public int ProductId { get; set; }

        //quantidade 
        public int Quantidade { get;set; }

        public DateTime LastUpdate { get; set; }
    }
}
