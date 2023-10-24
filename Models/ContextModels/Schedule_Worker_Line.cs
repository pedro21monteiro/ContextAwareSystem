using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Schedule_Worker_Line
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int Shift { get; set; }
        public int LineId { get; set; }
        public int? OperatorId { get; set; }
        public int? SupervisorId { get; set; }

    }
}
