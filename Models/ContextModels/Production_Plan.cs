using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Production_Plan
    {
        public int Id { get; set; }
        public int Goal { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Shift { get; set; }
        public int ProductId { get; set; }
        public int LineId { get; set; }

    }
}
