using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ContextModels
{
    public class ItensToUpdate
    {
        public List<Component> components { get; set; }
        public List<Coordinator> coordinators { get; set; }
        public List<Device> devices { get; set; }
        public List<Line> lines { get; set; }
        public List<Operator> operators { get; set; }
        public List<Product> products { get; set; }
        public List<Production> productions { get; set; }
        public List<Production_Plan> production_Plans { get; set; }
        public List<Reason> reasons { get; set; }
        public List<Request> requests { get; set; }
        public List<Schedule_Worker_Line> schedules { get; set; }
        public List<Stop> stops { get; set; }
        public List<Supervisor> supervisors { get; set; }
        public List<Worker> workers { get; set; }

    }
}
