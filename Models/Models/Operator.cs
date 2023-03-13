using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Operator : Worker
    {
        public int WorkerId { get; set; }

        List<Schedule_Worker_Line> listSchedules_WorkerLine { get; set; } = new List<Schedule_Worker_Line>();
        public void FillOperator(Worker w)
        {
            this.Id = w.Id;
            this.IdFirebase = w.IdFirebase;
            this.UserName = w.UserName;
            this.Email = w.Email;
            this.Role = w.Role;
        }
    }
}
