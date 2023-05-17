using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContinentalModels
{
    public class Component
    {
        public Component()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Reference { get; set; } = String.Empty;
        [Required]
        public int Category { get; set; }//0 - Sem categoria, 1-Etiqueta , 2- parafusos / etc...
        public DateTime LastUpdate { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Product> Products { get; set; }

        //parametros que não estão na bd
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
