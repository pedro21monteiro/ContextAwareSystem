using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Production_Plan
    {
        public Production_Plan()
        {
            this.Productions = new HashSet<Production>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public int Goal { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime InitialDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Shift { get; set; }
        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public Product Product { get; set; } = new Product();
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public Line Line { get; set; } = new Line();
        [ForeignKey("LineId")]
        public int LineId { get; set; }

        //------
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Production> Productions { get; set; }

    }
}
