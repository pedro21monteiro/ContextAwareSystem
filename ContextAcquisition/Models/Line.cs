using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextAcquisition.Models
{
    public class Line
    {
        public Line()
        {
            this.Production_Plans = new HashSet<Production_Plan>();
            this.Devices = new HashSet<Device>();
            this.Stops = new HashSet<Stop>();
            this.Schedules = new HashSet<Schedule_Worker_Line>();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool Priority { get; set; }

        public Coordinator Coordinator { get; set; } 
        public int CoordinatorId { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<Production_Plan> Production_Plans { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Stop> Stops { get; set; }
        public virtual ICollection<Schedule_Worker_Line> Schedules { get; set; }

        

    }
}
