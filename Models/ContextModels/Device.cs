using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Device
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int LineId { get; set; }
    }
}
