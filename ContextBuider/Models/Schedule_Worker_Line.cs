using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBuider.Models
{
    public class Schedule_Worker_Line
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public DateTime Day { get; set; }
        [Required]
        public int Shift { get; set; }

        //---
        public Line Line { get; set; } = new Line();
        public int LineId { get; set; }

        public Operator? Operator { get; set; }
        public int? OperatorId { get; set; }
        public Supervisor? Supervisor { get; set; }
        public int? SupervisorId { get; set; }

    }
}
