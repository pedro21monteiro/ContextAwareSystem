using ContextAcquisition.Data;
using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public interface ILogic
    {
        Task UpdateItens(ItensToUpdate ITU, DateTime? lastVerification);
    }
}
