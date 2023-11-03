using ContextAcquisition.Data;
using Models.FunctionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public interface ILogic
    {
        Task UpdateItensDMUD(ItensToUpdate ITU);
        Task UpdateItensCDC(ItensToUpdate ITU, DateTime lastVerification);
    }
}
