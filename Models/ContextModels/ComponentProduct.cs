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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual Component Component { get; set; } = new Component();
        [ForeignKey("ComponentId")]
        public int ComponentId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual Product Product { get; set; } = new Product();
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        //quantidade 
        public int Quantidade { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
