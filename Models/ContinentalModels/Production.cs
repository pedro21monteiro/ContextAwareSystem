﻿using System.ComponentModel.DataAnnotations;

namespace Models.ContinentalModels
{
    public class Production
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Production_Plan Prod_Plan { get; set; } = new Production_Plan();
        public int Production_PlanId { get; set; }
    }
}
