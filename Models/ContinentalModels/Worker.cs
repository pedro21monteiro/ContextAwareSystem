﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContinentalModels
{
    public class Worker
    {
        public Worker()
        {
            this.Coordinators = new HashSet<Coordinator>();
            this.Operators = new HashSet<Operator>();
            this.Supervisors = new HashSet<Supervisor>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string IdFirebase { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Role { get; set; } //1-coordinator , 2- Operator , 3 - Supervisor

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Coordinator> Coordinators { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Operator> Operators { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Supervisor> Supervisors{ get; set; }
    }
}
