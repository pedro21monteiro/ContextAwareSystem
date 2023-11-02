using Models.cdc_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ContextModels
{
    public class ItensToUpdate
    {
        //Usar o método do DataMonitoringUsingDatabase
        public List<Production> productions { get; set; } = new List<Production>();
        public List<Stop> stops { get; set; } = new List<Stop>();

        //Usar o cdc do DataMonitoringUsingDatabase
        public List<CDC_Production> Cdc_Productions { get; set; } = new List<CDC_Production>();
        public List<CDC_Stop> Cdc_Stops { get; set; } = new List<CDC_Stop>();
    }
}
