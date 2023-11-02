using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.cdc_Models
{
    public class CDC_Production
    {
        [Key]
        public int Id { get; set; }
        public int IdProduction { get; set; }
        public int Hour { get; set; }
        public DateTime Day { get; set; }
        public int Quantity { get; set; }
        public int Production_PlanId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int Operation { get; set; }//1-delete, 2-insert, 3-update
    }
}
