using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextAcquisition.Models
{
    public class Worker
    {
        public Worker()
        {
            this.Coordinators = new HashSet<Coordinator>();
            this.Operators = new HashSet<Operator>();
            this.Supervisors = new HashSet<Supervisor>();
            this.Requests = new HashSet<Request>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string IdFirebase { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Role { get; set; } //1-coordinator , 2- Operator , 3 - Supervisor
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<Coordinator> Coordinators { get; set; }
        public virtual ICollection<Operator> Operators { get; set; }
        public virtual ICollection<Supervisor> Supervisors{ get; set; }
        public virtual ICollection<Request>  Requests{ get; set; }
    }
}
