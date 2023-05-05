using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Stop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public bool Planned { get; set; }
        [Required]
        public DateTime InitialDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int Shift { get; set; }
        public DateTime LastUpdate { get; set; }

        //----
        [JsonIgnore]
        [IgnoreDataMember]
        public Line Line { get; set; } = new Line();
        public int LineId { get; set; }

        public Reason? Reason { get; set; }
        public int? ReasonId { get; set; }

    }
}
