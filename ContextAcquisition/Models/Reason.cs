using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextAcquisition.Models
{
    public class Reason
    {
        public Reason()
        {
            this.Stops = new HashSet<Stop>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

        //--
        public virtual ICollection<Stop> Stops{ get; set; }
    }
}
