using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonWorker
    {
        public int Id { get; set; }
        public string IdFirebase { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; } 
        public DateTime LastUpdate { get; set; }
    }
}
