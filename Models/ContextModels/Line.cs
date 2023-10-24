using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Priority { get; set; }
        public int CoordinatorId { get; set; }
       
    }
}
