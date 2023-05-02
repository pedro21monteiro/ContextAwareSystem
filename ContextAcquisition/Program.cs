using ContextAcquisition.Data;

namespace ContextAcquisition
{
    class Program
    {

        static void Main(string[] args)
        {
            using var _context = new ContextAcquisitonDb();
            
        }

    }

}