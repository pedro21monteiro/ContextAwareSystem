using ContextBuilder.Data;

namespace ContextBuilder
{
    public class DataManagement : BackgroundService
    {
        //private const int generalDelay = 24 * 60 * 10 * 1000 * 6; // 60 minuto = 1 hora *24
        private const int generalDelay =  10 * 1000 * 2;//1 minuto
        private IServiceProvider _sp;
        public DataManagement(IServiceProvider sp)
        {
            _sp = sp;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(generalDelay, stoppingToken);

                //de 24 em 24 horas vai ver os requests e eliminar os que foram à mais de 1 ano e 8 meses
                Console.WriteLine(DateTime.Now.ToString());
                try
                {
                    await CleanRequests();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        private Task CleanRequests()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ContextBuilderDb>();
                bool alterou = false;
                foreach (var request in _context.Requests)
                {
                    TimeSpan ts = DateTime.Now.Subtract(request.Date);
                    //1 ano e 8 meses
                    if (ts.Days > 605)
                    {
                        _context.Requests.Remove(request);
                        alterou = true;
                        Console.WriteLine("Request: " + request.Id.ToString() + " - Removido com Sucesso");
                    }
                }
                if(alterou == true)
                {
                    _context.SaveChangesAsync();
                }
                
            }
            return Task.CompletedTask;
            //return Task.FromResult("Done");
        }
    }
}
