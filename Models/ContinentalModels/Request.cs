using System.ComponentModel.DataAnnotations;

namespace Models.ContinentalModels
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Worker Worker { get; set; } 
        public int WorkerId { get; set; }
        public int LineId { get; set; }
        public string Device { get; set; } = string.Empty;

        public DateTime LastUpdate { get; set; }

    }
}
