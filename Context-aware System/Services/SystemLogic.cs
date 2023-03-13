using Models.FunctionModels;

namespace Context_aware_System.Services
{
    public class SystemLogic : ISystemLogic
    {
        //funções da logica do sistema
        public bool dateTimeIsActiveNow(DateTime dtInitial, DateTime dtFinal)
        {
            DateTime now = DateTime.Now;
            if(now.CompareTo(dtInitial)>=0 && now.CompareTo(dtFinal) <= 0)
            {
                return true;
            }
            return false;
        }

        public WorkShift GetAtualWorkShift(DateTime dt)
        {
            WorkShift ws = new WorkShift();

            int hour = dt.Hour;
            if (hour >= 6 && hour < 15)
            {
                ws.Shift = 1;
                ws.ShiftString = "Turno da Manha, Dás 06:00 às 15:00";
                ws.InitialDate = new DateTime(dt.Year, dt.Month, dt.Hour, 6, 0, 0);
                ws.EndDate = new DateTime(dt.Year, dt.Month, dt.Hour, 14, 59, 59);
            }
            if (hour >= 15 && hour < 24)
            {
                ws.Shift = 2;
                ws.ShiftString = "Turno da Tarde, Dás 15:00 às 00:00";
                ws.InitialDate = new DateTime(dt.Year, dt.Month, dt.Hour, 15, 0, 0);
                ws.EndDate = new DateTime(dt.Year, dt.Month, dt.Hour, 23, 59, 59);
            }
            if (hour >= 0 && hour < 6)
            {  
                ws.Shift = 3;
                ws.ShiftString = "Turno da Noite, Dás 00:00 às 06:00";
                ws.InitialDate = new DateTime(dt.Year, dt.Month, dt.Hour, 0, 0, 0);
                ws.EndDate = new DateTime(dt.Year, dt.Month, dt.Hour, 5, 59, 59);
            }

            return ws;
        }

        public bool IsAtributeInDatetime(DateTime? dtSearchInitial, DateTime? dtSearchFinal, DateTime dtInitial, DateTime dtFinal)
        {
            //se os dois não tiverem valores
            if (!dtSearchInitial.HasValue && !dtSearchFinal.HasValue)
            {
                //vai retornar todos
                return true;
            }
            //se os dois tiverem valor:
            if (dtSearchInitial.HasValue && dtSearchFinal.HasValue)
            {
                if ((dtInitial.CompareTo(dtSearchInitial) < 0 && dtFinal.CompareTo(dtSearchInitial) < 0) || (dtInitial.CompareTo(dtSearchFinal) > 0 && dtFinal.CompareTo(dtSearchFinal) > 0))
                {
                    return false;
                }
                return true;
            }
            //se tiver data inicial e não tiver final
            if (dtSearchInitial.HasValue && !dtSearchFinal.HasValue)
            {
                if ((dtInitial.CompareTo(dtSearchInitial) < 0 && dtFinal.CompareTo(dtSearchInitial) < 0) || (dtInitial.CompareTo(DateTime.Now) > 0 && dtFinal.CompareTo(DateTime.Now) > 0))
                {
                    return false;
                }
                return true;
            }
            //Se tiver só a final
            if (!dtSearchInitial.HasValue && dtSearchFinal.HasValue)
            {
                if ((dtInitial.CompareTo(new DateTime()) < 0 && dtFinal.CompareTo(new DateTime()) < 0) || (dtInitial.CompareTo(dtSearchFinal) > 0 && dtFinal.CompareTo(dtSearchFinal) > 0))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
