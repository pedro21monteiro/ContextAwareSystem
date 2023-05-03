﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextAcquisition.Models
{
    public class Supervisor 
    {
        public Supervisor()
        {
            this.Schedules = new HashSet<Schedule_Worker_Line>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public Worker Worker { get; set; }
        public int WorkerId { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual ICollection<Schedule_Worker_Line> Schedules { get; set; }
    }
}
