using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.cdc_Models
{
    public class CDC_Stop
    {
        [Key]
        public int Id { get; set; }
        public int IdStop { get; set; }
        public bool Planned { get; set; }
        public DateTime InitialDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; }
        public int Shift { get; set; }
        public int LineId { get; set; }
        public int? ReasonId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int Operation { get; set; }//1-delete, 2-insert, 3 update


        public Stop toStop()
        {
            Stop stop = new Stop();
            stop.Id = this.IdStop;
            stop.Planned = this.Planned;
            stop.InitialDate = this.InitialDate;
            stop.EndDate = this.EndDate;
            stop.Duration = this.Duration;
            stop.Shift = this.Shift;
            stop.LineId = this.LineId;
            stop.ReasonId = this.ReasonId;

            return stop;
        }
    }
}
