using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ContextModels
{
    public class LastVerificationRegist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public DateTime ComponentsVerification { get; set; }
        public DateTime CoordinatorsVerification { get; set; }
        public DateTime DevicesVerification { get; set; }
        public DateTime LinesVerification { get; set; }
        public DateTime OperatorsVerification { get; set; }
        public DateTime ProductsVerification { get; set; }
        public DateTime ProductionsVerification { get; set; }
        public DateTime ProductionPlansVerification { get; set; }
        public DateTime ReasonsVerification { get; set; }
        public DateTime RequestsVerification { get; set; }
        public DateTime Schedule_worker_linesVerification { get; set; }
        public DateTime StopsVerification { get; set; }
        public DateTime SupervisorsVerification { get; set; }
        public DateTime WorkersVerification { get; set; }
    }
}
