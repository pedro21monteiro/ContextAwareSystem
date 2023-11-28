using ContextBuilder.Data;

namespace ContextBuilder
{
    public class DataManagement : BackgroundService
    {
        private const int generalDelay =  1000 * 60 * 60 * 24;//24 horas
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
                Console.WriteLine(DateTime.Now.ToString());
                try
                {
                    await CleanRequests();
                    await CleanMissingComponents();
                    await CleanAlertHistories();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public async Task CleanRequests()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContextBuilderDb>();
                foreach (var request in _context.Requests)
                {
                    TimeSpan ts = DateTime.Now.Subtract(request.Date);
                    //1 ano e 8 meses
                    if (ts.TotalDays > 605)
                    {
                        _context.Requests.Remove(request);
                        await _context.SaveChangesAsync();
                        Console.WriteLine("Request: " + request.Id.ToString() + " - Removido com Sucesso");
                    }
                }
            }
            return;
        }
        private async Task CleanMissingComponents()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContextBuilderDb>();
                foreach (var missingComponent in _context.missingComponents)
                {
                    TimeSpan ts = DateTime.Now.Subtract(missingComponent.OrderDate);
                    if (ts.TotalDays > 30)
                    {
                        _context.missingComponents.Remove(missingComponent);
                        await _context.SaveChangesAsync();
                        Console.WriteLine("MissingComponent: " + missingComponent.Id + " - Removido com Sucesso");
                    }
                }
            }
            return;
        }

        private async Task CleanAlertHistories()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContextBuilderDb>();
                foreach (var alertHistorie in _context.alertsHistories)
                {
                    TimeSpan ts = DateTime.Now.Subtract(alertHistorie.AlertDate);
                    if (ts.TotalDays > 90)
                    {
                        _context.alertsHistories.Remove(alertHistorie);
                        await _context.SaveChangesAsync();
                        Console.WriteLine("Alert: " + alertHistorie.Id + " - Removido com Sucesso");
                    }
                }
            }
            return;
        }
    }
}
