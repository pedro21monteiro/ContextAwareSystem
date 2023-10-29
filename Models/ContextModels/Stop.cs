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
        public bool Planned { get; set; }
        public DateTime InitialDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; }
        public int Shift { get; set; }
        public int LineId { get; set; } 
        public int? ReasonId { get; set; }
    }
}
