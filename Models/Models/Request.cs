﻿using System.ComponentModel.DataAnnotations;

namespace Models.Models
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

    }
}