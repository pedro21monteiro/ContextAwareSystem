using Models.FunctionModels;

namespace Context_aware_System.Services
{
    public interface ISystemLogic
    {
        bool dateTimeIsActiveNow(DateTime dtInitial, DateTime dtFinal);

        WorkShift GetAtualWorkShift(DateTime dt);

        bool IsAtributeInDatetime(DateTime? dtSearchInitial, DateTime? dtSearchFinal, DateTime dtInitial, DateTime dtFinal);

    }
}
