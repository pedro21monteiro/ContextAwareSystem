using ContextAcquisition.Data;
using ContextAcquisition.Services;
using Models.ContextModels;
using System.Net.Http;

namespace ContextAcquisition
{
    class Program
    {

        static async Task Main(string[] args)
        {
            using var _context = new ContextAcquisitonDb();
            
            HttpClient client = new HttpClient();

            Service _service = new Service(client);
            
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(-1);
            List<Component> components = _service.GetComponents(dateTime).Result;
            Console.WriteLine(dateTime.ToString());
            if (components != null)
            {
                foreach (Component c in components)
                {
                    Console.WriteLine(c.Id);
                }
            }
            

            await Task.Run(() => Thread.Sleep(Timeout.Infinite));
        }   

    }

}