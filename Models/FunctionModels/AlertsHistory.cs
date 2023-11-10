using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FunctionModels
{
    public class AlertsHistory
    {
        [Key]
        public int Id { get; set; }

        //1-Alerta de Paragem, 2- Alerta de produções, 3- Alerta de componentes em falta
        public int TypeOfAlet { get; set; }
        public bool AlertSuccessfullySent { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string AlertMessage { get; set; } = string.Empty;
        public DateTime AlertDate { get; set; }
    }
}
