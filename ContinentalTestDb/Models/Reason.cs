using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ContinentalTestDb.Models
{
    public class Reason
    {
        public Reason()
        {
            this.Stops = new HashSet<Stop>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

        //--
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Stop> Stops{ get; set; }
    }
}
