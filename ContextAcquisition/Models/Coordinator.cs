using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextAcquisition.Models
{
    public class Coordinator 
    {
        public Coordinator()
        {
            this.Lines = new HashSet<Line>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }

        public virtual ICollection<Line> Lines { get; set; }
    }
}
