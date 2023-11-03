using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.FunctionModels
{
    public class MissingComponent
    {
        [JsonIgnore]
        [IgnoreDataMember]
        [Key]
        public int Id { get; set; }
        public int LineId { get; set; }
        public int ComponentId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
