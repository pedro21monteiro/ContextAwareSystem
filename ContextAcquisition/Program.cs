using ContextAcquisition.Data;
using ContextAcquisition.Services;
using Models.ContextModels;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.EntityFrameworkCore;

namespace ContextAcquisition
{
    class Program
    {
        public static System.Timers.Timer _timer;
        public static ContextAcquisitonDb _context = new ContextAcquisitonDb();
        public static HttpClient client = new HttpClient();
        public static Service _service = new Service(client);
        static async Task Main(string[] args)
        {
            //inicializar o contexto para haver uma primeira data de pesquisa
            DbInitializer.Initalize(_context);
            

            //Inicialmente vou começar com um Timer para todas as funções esse timer será de 30 em 30 segundos
            Console.WriteLine(DateTime.Now);
            _timer = new System.Timers.Timer();
            _timer.AutoReset = false;
            _timer.Interval = 30000; // Intervalo em milésimos
            // _timer.Elapsed += new System.Timers.ElapsedEventHandler(await executarTarefaAsync());
            _timer.Elapsed += async (sender, arq) => await executarTarefaAsync(sender,arq);
            _timer.Enabled = true;

            await Task.Run(() => Thread.Sleep(Timeout.Infinite));
        }

        static async Task executarTarefaAsync(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            //--------------
            //buscar as datas de ultima verificação
            var lvr = _context.LastVerificationRegists.First();
            if(lvr == null)
            {
                Console.WriteLine("Erro ao inicializar a BD");
                return;
            }

            ItensToUpdate ITU = new ItensToUpdate();

            //itens para adicionar/atualizar
            ITU.components = _service.GetComponents(lvr.ComponentsVerification).Result;
            ITU.coordinators = _service.GetCoordinators(lvr.CoordinatorsVerification).Result;
            ITU.devices = _service.GetDevices(lvr.DevicesVerification).Result;
            ITU.lines = _service.GetLines(lvr.LinesVerification).Result;
            ITU.operators = _service.GetOperators(lvr.OperatorsVerification).Result;
            ITU.products = _service.GetProducts(lvr.ProductsVerification).Result;
            ITU.productions = _service.GetProductions(lvr.ProductionsVerification).Result;
            ITU.production_Plans = _service.GetProductionPlans(lvr.ProductionPlansVerification).Result;
            ITU.reasons = _service.GetReasons(lvr.ReasonsVerification).Result;
            //ITU.requests = _service.GetRequests(lvr.RequestsVerification).Result;
            ITU.schedules = _service.GetSchedule_Worker_Lines(lvr.Schedule_worker_linesVerification).Result;
            ITU.stops = _service.GetStops(lvr.StopsVerification).Result;
            ITU.supervisors = _service.GetSupervisors(lvr.SupervisorsVerification).Result;
            ITU.workers = _service.GetWorkers(lvr.WorkersVerification).Result;
            ITU.ComponentProducts = _service.GetComponentProducts(lvr.WorkersVerification).Result;

            //Atualizar a lista da base de dados
            await Service.UpdateItens(ITU, _context);
           

            //---------------
            _timer.Enabled = true;
        }


    }

}