using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ContinentalTestAPI.Models
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
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool Priority { get; set; }

        public Coordinator Coordinator { get; set; } 
        public int CoordinatorId { get; set; }

        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Production_Plan> Production_Plans { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Device> Devices { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Stop> Stops { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Schedule_Worker_Line> Schedules { get; set; }

        

    }
}
