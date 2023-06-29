using ContextAcquisition.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public class Testes
    {
        public static ContextAcquisitonDb _context = new ContextAcquisitonDb();

        public static bool ComponentTest()
        {
            if (_context.Components.ToList().Any())
            {
                return true;
            }
            return false;
        }
    }
}
