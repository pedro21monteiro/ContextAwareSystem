using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextAcquisition.Models
{
    public class Component
    {
        public Component()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Reference { get; set; } = String.Empty;
        [Required]
        public int Category { get; set; }//0 - Sem categoria, 1-Etiqueta , 2- parafusos / etc...

        public virtual ICollection<Product> Products { get; set; }

    }
}
