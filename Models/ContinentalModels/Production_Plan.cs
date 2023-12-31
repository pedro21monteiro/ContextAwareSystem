﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContinentalModels
{
    public class Production_Plan
    {
        public Production_Plan()
        {
            this.Productions = new HashSet<Production>();
        }

        [Key]
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
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Production> Productions { get; set; }
        public Product Product { get; set; } = new Product();
        public int ProductId { get; set; }
        public Line Line { get; set; } = new Line();
        public int LineId { get; set; }

    }
}
