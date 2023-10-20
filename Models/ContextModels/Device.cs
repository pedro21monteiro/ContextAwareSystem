using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }

        //---
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public Line Line { get; set; } = new Line();
        [ForeignKey("LineId")]
        public int LineId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
