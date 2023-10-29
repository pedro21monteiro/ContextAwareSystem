using ContextAcquisition.Data;
using ContextAcquisition.Services;
using Models.ContextModels;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.DataServices;

namespace ContextAcquisition
{
    class Program
    {
        //Vai haver duas funcionalidades para fazer a verificação de alteração de dados 1-DataMonitoringUsingDatabase, 2-CDC(Change Data Capture)
        public static int functionDataMonitoring = 1;

        public static System.Timers.Timer _timer;
        //Singleton para o ContextAcquisitonDb
        private static readonly ContextAcquisitonDb _context = new ContextAcquisitonDb();
        public static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {            
            //inicializar o contexto para haver uma primeira data de pesquisa
            DbInitializer.Initalize(_context);
            //criar o singleton para o service de Data
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IDataService, DataService>()
            .BuildServiceProvider();
            var dataService = serviceProvider.GetRequiredService<IDataService>();
            //criar o singleton para o service de Logic
            var serviceProvider2 = new ServiceCollection()
            .AddSingleton<ILogic, Logic>()
            .BuildServiceProvider();
            var logicService = serviceProvider2.GetRequiredService<ILogic>();


            //Inicialmente vou começar com um Timer para todas as funções esse timer será de 30 em 30 segundos
            _timer = new System.Timers.Timer();
            _timer.AutoReset = false;
            _timer.Interval = 30000; // Intervalo em milésimos 30000(30 segundos)

            if(functionDataMonitoring == 1)
            {
                _timer.Elapsed += async (sender, arq) => await DataMonitoringUsingDatabase(sender, arq, dataService, logicService);
            }
            if (functionDataMonitoring == 2)
            {
                //adicionar e criar o cdc
            }

            _timer.Enabled = true;
            await Task.Run(() => Thread.Sleep(Timeout.Infinite));
        }

        static async Task DataMonitoringUsingDatabase(object sender, System.Timers.ElapsedEventArgs e, IDataService _dataService, ILogic _logicService)
        {
            _timer.Enabled = false;
            ItensToUpdate ITU = new ItensToUpdate();
            //--------------
            //vai buscar os dados que são para analisar (productions e stops)
            //em cada um vamos ter de verificar a inserção e update de dados, os deletes não interessam
            var stops = await _dataService.GetStops(null,null,null,null,null,null,null,null);
            if (stops != null)
            {
                //fazer a deteção de alteração nos stops
                foreach(var stop in stops)
                {
                    //ver se o stop existe na base de dados
                    if(!_context.Stops.Contains(stop))
                    {
                        //se não existir foi detetada uma modificação de nova inserção ou update neste stop
                        ITU.stops.Add(stop);
                    }
                }
            }
            //Productions
            var productions = await _dataService.GetProductions(null,null,null,null,null);
            if (productions != null)
            {
                //fazer a deteção de alteração nos stops
                foreach (var production in productions)
                {
                    //ver se o stop existe na base de dados
                    if (!_context.Productions.Contains(production))
                    {
                        //se não existir foi detetada uma modificação de nova inserção ou update neste stop
                        ITU.productions.Add(production);
                    }
                }
            }
            await _logicService.UpdateItens(ITU, null);
            //---------------
            _timer.Enabled = true;
        }

        //static async Task DataMonitoringUsingDatabase(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    _timer.Enabled = false;
        //    //--------------
        //    //buscar as datas de ultima verificação
        //    var lvr = _context.LastVerificationRegists.First();
        //    if (lvr == null)
        //    {
        //        Console.WriteLine("Erro ao inicializar a BD");
        //        return;
        //    }

        //    ItensToUpdate ITU = new ItensToUpdate();

        //    //itens para adicionar/atualizar
        //    ITU.components = _service.GetComponents(lvr.ComponentsVerification).Result;
        //    ITU.coordinators = _service.GetCoordinators(lvr.CoordinatorsVerification).Result;
        //    ITU.devices = _service.GetDevices(lvr.DevicesVerification).Result;
        //    ITU.lines = _service.GetLines(lvr.LinesVerification).Result;
        //    ITU.operators = _service.GetOperators(lvr.OperatorsVerification).Result;
        //    ITU.products = _service.GetProducts(lvr.ProductsVerification).Result;
        //    ITU.productions = _service.GetProductions(lvr.ProductionsVerification).Result;
        //    ITU.production_Plans = _service.GetProductionPlans(lvr.ProductionPlansVerification).Result;
        //    ITU.reasons = _service.GetReasons(lvr.ReasonsVerification).Result;
        //    //ITU.requests = _service.GetRequests(lvr.RequestsVerification).Result;
        //    ITU.schedules = _service.GetSchedule_Worker_Lines(lvr.Schedule_worker_linesVerification).Result;
        //    ITU.stops = _service.GetStops(lvr.StopsVerification).Result;
        //    ITU.supervisors = _service.GetSupervisors(lvr.SupervisorsVerification).Result;
        //    ITU.workers = _service.GetWorkers(lvr.WorkersVerification).Result;
        //    ITU.ComponentProducts = _service.GetComponentProducts(lvr.WorkersVerification).Result;

        //    //Atualizar a lista da base de dados
        //    await Service.UpdateItens(ITU, _context);


        //    //---------------
        //    _timer.Enabled = true;
        //}

    }

}