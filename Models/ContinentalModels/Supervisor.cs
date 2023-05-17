﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContinentalModels
{
    public class Supervisor 
    {
        public Supervisor()
        {
            this.Schedules = new HashSet<Schedule_Worker_Line>();
        }

        [Key]
        public int Id { get; set; }

        public Worker Worker { get; set; }
        public int WorkerId { get; set; }
        public DateTime LastUpdate { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Schedule_Worker_Line> Schedules { get; set; }
    }
}