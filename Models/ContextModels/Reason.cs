using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
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
        public DateTime LastUpdate { get; set; }

        //--
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Stop> Stops{ get; set; }
    }
}
