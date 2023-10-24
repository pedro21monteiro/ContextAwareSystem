using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
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
