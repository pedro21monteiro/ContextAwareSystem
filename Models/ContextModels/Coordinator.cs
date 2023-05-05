using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
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
        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Line> Lines { get; set; }
    }
}
