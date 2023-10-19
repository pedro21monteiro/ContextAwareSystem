﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string IdFirebase { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Role { get; set; } //1-coordinator , 2- Operator , 3 - Supervisor
        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Coordinator> Coordinators { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Operator> Operators { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public virtual ICollection<Supervisor> Supervisors{ get; set; }
    }
}
