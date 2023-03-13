using Context_aware_System.ContextDbModels;

namespace Context_aware_System.Data
{
    public class DbInitializer
    {
        public static void Initalize(ContextAwareDataBaseContext _context)
        {
            _context.Database.EnsureCreated();

            //StopVerification
            if (_context.StopsVerifications.Any() == false)
            {
                var stopsVerifications = new StopsVerification[]
                {
                new StopsVerification{VerificationDate = DateTime.Now, NewStopsCount = 0 },

                };
                foreach (StopsVerification sv in stopsVerifications)
                {
                    _context.StopsVerifications.Add(sv);
                }
                _context.SaveChanges();
            } 
            //Inicializar outras :

        }
    }
}
