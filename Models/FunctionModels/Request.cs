using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.FunctionModels
{
    public class Request
    {
        [JsonIgnore]
        [IgnoreDataMember]
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public int WorkerId { get; set; }
        public int LineId { get; set; }

    }
}
