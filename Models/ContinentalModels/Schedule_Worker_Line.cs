using System.ComponentModel.DataAnnotations;

namespace Models.ContinentalModels
{
    public class Schedule_Worker_Line
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [Required]
        public int Shift { get; set; }
        public Line Line { get; set; } = new Line();
        public int LineId { get; set; }
        public Operator? Operator { get; set; }
        public int? OperatorId { get; set; }
        public Supervisor? Supervisor { get; set; }
        public int? SupervisorId { get; set; }
    }
}
