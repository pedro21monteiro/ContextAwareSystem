using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Data
{
    public class DbInitializer
    {
        public static void Initalize(ContextAcquisitonDb context)
        {
            context.Database.EnsureCreated();

            if (context.LastVerificationRegists.Any())
            {
                return;
            }
            LastVerificationRegist lvr = new LastVerificationRegist();
            lvr.ProductionsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.StopsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            context.LastVerificationRegists.Add(lvr);
            context.SaveChanges();
        }
    }
}
