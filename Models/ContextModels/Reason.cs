using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Reason
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
