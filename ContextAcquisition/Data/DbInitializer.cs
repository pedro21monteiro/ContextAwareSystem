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
            lvr.ComponentsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.CoordinatorsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.DevicesVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.LinesVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.OperatorsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.ProductsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.ProductionsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.ProductionPlansVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.ReasonsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.RequestsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.Schedule_worker_linesVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.StopsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.SupervisorsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.WorkersVerification = new DateTime(1, 1, 1, 1, 1, 1);
            lvr.ComponentProductsVerification = new DateTime(1, 1, 1, 1, 1, 1);
            context.LastVerificationRegists.Add(lvr);
            context.SaveChanges();

            
        }
    }
}
