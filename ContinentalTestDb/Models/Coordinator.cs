using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ContinentalTestDb.Models
{
    public class Coordinator 
    {
        public Coordinator()
        {
            this.Lines = new HashSet<Line>();
        }
        [Key]
        public int Id { get; set; }
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Line> Lines { get; set; }
    }
}
