using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string IdFirebase { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; } //1-coordinator , 2- Operator , 3 - Supervisor
    }
}
