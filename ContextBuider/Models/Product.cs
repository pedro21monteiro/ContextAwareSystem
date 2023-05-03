using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBuider.Models
{
    public class Product
    {
        public Product()
        {
            this.Components = new HashSet<Component>();
            this.Production_Plans = new HashSet<Production_Plan>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string LabelReference { get; set; } = String.Empty;
        [Required]
        public TimeSpan Cycle { get; set; }

        public DateTime LastUpdate { get; set; }

        public virtual ICollection<Component> Components { get; set; }

        public virtual ICollection<Production_Plan> Production_Plans { get; set; }

    }
}
