using ContextBuilder.Data;
using Models.FunctionModels;

namespace ContextBuilder
{
    public class DataManagement : BackgroundService
    {
        private const int generalDelay =  1000 * 30;//24 horas- 1000 * 60 * 60 * 24;//24 horas
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
        /// <summary>
        /// Função responsável por fazer a limpeza de Requests com datas que deixam de ser considerada importates para a aplicação.
        /// </summary>
        private async Task CleanRequests()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContextBuilderDb>();

                var requestsToRemove = new List<Request>();
                foreach (var request in _context.Requests)
                {
                    TimeSpan ts = DateTime.Now.Subtract(request.Date);
                    if (ts.TotalDays > 605)
                    {
                        requestsToRemove.Add(request);
                    }
                }

                foreach (var request in requestsToRemove)
                {
                    _context.Requests.Remove(request);
                    Console.WriteLine($"Request: {request.Id} - Removido com Sucesso.");
                }
                await _context.SaveChangesAsync();
            }
            return;
        }
        /// <summary>
        /// Função responsável por fazer a limpeza de componentes em falta com datas que deixam de ser consideradas importates para a aplicação.
        /// </summary>
        private async Task CleanMissingComponents()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContextBuilderDb>();

                var missingComponentsToRemove = new List<MissingComponent>();
                foreach (var missingComponent in _context.missingComponents)
                {
                    TimeSpan ts = DateTime.Now.Subtract(missingComponent.OrderDate);
                    if (ts.TotalDays > 30)
                    {
                        missingComponentsToRemove.Add(missingComponent);
                    }
                }

                foreach(var missingComponent in missingComponentsToRemove)
                {
                    _context.missingComponents.Remove(missingComponent);
                    Console.WriteLine($"MissingComponent: {missingComponent.Id} - Removido com Sucesso.");
                }
                await _context.SaveChangesAsync();
            }
            return;
        }
        /// <summary>
        /// Função responsável por fazer a limpeza de gistóricos de alertas com datas que deixam de ser considerada importates para a aplicação.
        /// </summary>
        private async Task CleanAlertHistories()
        {
            using (var scope = _sp.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IContextBuilderDb>();

                var alertsHistorieToRemove = new List<AlertsHistory>();
                foreach (var alertHistory in _context.alertsHistories)
                {
                    TimeSpan ts = DateTime.Now.Subtract(alertHistory.AlertDate);
                    if (ts.TotalDays > 90)
                    {
                        alertsHistorieToRemove.Add(alertHistory);
                    }
                }
                foreach(var alertHistory in alertsHistorieToRemove)
                {
                    _context.alertsHistories.Remove(alertHistory);
                    Console.WriteLine($"Alert: {alertHistory.Id} - Removido com Sucesso.");
                }
                await _context.SaveChangesAsync();
            }
            return;
        }
    }
}
