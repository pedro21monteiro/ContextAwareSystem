﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBuider.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Worker Worker { get; set; } 
        public int WorkerId { get; set; }
        public int LineId { get; set; }
        public string Device { get; set; } = string.Empty;
    }
}
