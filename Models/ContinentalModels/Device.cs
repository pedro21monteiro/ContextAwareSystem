using System.ComponentModel.DataAnnotations;

namespace Models.ContinentalModels
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }

        //---
        public Line Line { get; set; } = new Line();
        public int LineId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
