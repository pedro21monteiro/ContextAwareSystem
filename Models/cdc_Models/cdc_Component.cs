using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.cdc_Models
{
    public class cdc_Component
    {
        [Key]
        public int Id { get; set; }
        public int IdComponent { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Reference { get; set; } = String.Empty;
        public int Category { get; set; }
        public DateTime ModificationDate { get; set; }
        public int Operation { get; set; }//1-delete, 2-insert, 3 update

    }
}
