using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Coordinator : Worker
    {
        public int WorkerId { get; set; }

        public List<Line> listLinesResponsable { get; set; } = new List<Line>();


        public void FillWorker(Worker w)
        {
            this.Id = w.Id;
            this.IdFirebase = w.IdFirebase;
            this.UserName = w.UserName;
            this.Email = w.Email;
            this.Role = w.Role;
        }
    }
}
