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
            //this.Products = new HashSet<Product>();
            this.ComponentProducts = new HashSet<ComponentProduct>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Reference { get; set; } = String.Empty;
        [Required]
        public int Category { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ComponentProduct> ComponentProducts { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
