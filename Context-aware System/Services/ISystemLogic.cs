using Models.FunctionModels;

namespace ContextServer.Services
{
    public interface ISystemLogic
    {
        bool dateTimeIsActiveNow(DateTime dtInitial, DateTime dtFinal);

        WorkShift GetAtualWorkShift(DateTime dt);

        bool IsAtributeInDatetime(DateTime? dtSearchInitial, DateTime? dtSearchFinal, DateTime dtInitial, DateTime dtFinal);
        bool IsAtributeInDatetime(DateTime? dtSearchInitial, DateTime? dtSearchFinal, DateTime Day);
    }
}
