using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
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
        public DateTime LastUpdate { get; set; }

        //---
        //[JsonIgnore]
        //[IgnoreDataMember]
        [NotMapped]
        public Production_Plan Prod_Plan { get; set; } = new Production_Plan();
        [ForeignKey("Production_PlanId")]
        public int Production_PlanId { get; set; }
    }
}
