using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
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
        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public Coordinator Coordinator { get; set; }
        [ForeignKey("CoordinatorId")]
        public int CoordinatorId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Production_Plan> Production_Plans { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Device> Devices { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Stop> Stops { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Schedule_Worker_Line> Schedules { get; set; }

        

    }
}
