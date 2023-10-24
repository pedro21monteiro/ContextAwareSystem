using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string LabelReference { get; set; } = String.Empty;
        public TimeSpan Cycle { get; set; }

    }
}
