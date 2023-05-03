using System.ComponentModel.DataAnnotations;

namespace ContinentalTestDb.Models
{
    public class Stop
    {
        [Key]
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
        public Line Line { get; set; } = new Line();
        public int LineId { get; set; }

        public Reason? Reason { get; set; }
        public int? ReasonId { get; set; }

    }
}
