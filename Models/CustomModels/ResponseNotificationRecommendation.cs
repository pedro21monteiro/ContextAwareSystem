using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseNotificationRecommendation
    {
        public string Message { get; set; } = String.Empty;//Mensagem de erro
        //O turno esta dividido em duas partes
        public int ShiftSlipt { get; set; }
        public bool ExistSchedule { get; set; }
        //testes ao serviço
        public DateTime nextDate { get; set; }
        public int ParteTurno { get; set; }
        public float ParteTurno1 { get; set; }
        public float ParteTurno2 { get; set; }
        public int days { get; set; }
    }
}
