using ContextBuider.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ContextModels;

namespace ContextBuider.Services
{
    public interface ILogic
    {
        Task readMessage(string routingKey, string message, ContextAwareDb _context);

    }
}
