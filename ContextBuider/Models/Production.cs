using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBuider.Models
{
    public class Production
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        public DateTime Day { get; set; }
        [Required]
        public int Quantity { get; set; }

        //---
        public Production_Plan Prod_Plan { get; set; } = new Production_Plan();
        public int Production_PlanId { get; set; }
    }
}
