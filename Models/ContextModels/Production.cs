using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Production
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public DateTime Day { get; set; }
        public int Quantity { get; set; }
        public int Production_PlanId { get; set; }
    }
}
