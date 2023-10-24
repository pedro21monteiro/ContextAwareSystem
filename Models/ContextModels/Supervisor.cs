using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Supervisor 
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
    }
}
