using ContextAcquisition.Data;
using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public class Test
    {
        public static ContextAcquisitonDb _context = new ContextAcquisitonDb();
        public static HttpClient client = new HttpClient();
        public static Service _service = new Service(client);


        public static bool ComponenteTeste()
        {

            return true;
        }
    }
}
