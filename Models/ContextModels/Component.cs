using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.ContextModels
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Reference { get; set; } = String.Empty;
        public int Category { get; set; }

    }
}
