using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Operator 
    {
        public Operator()
        {
            this.Schedules = new HashSet<Schedule_Worker_Line>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public Worker Worker { get; set; }
        [ForeignKey("WorkerId")]
        public int WorkerId { get; set; }
        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Schedule_Worker_Line> Schedules { get; set; }
    }
}
