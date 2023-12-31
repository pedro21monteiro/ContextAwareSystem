﻿using ContextAcquisition.Data;
using ContextAcquisition.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.DataServices;
using Models.FunctionModels;

namespace ContextAcquisition
{
    class Program
    {
        //Vai haver duas funcionalidades para fazer a verificação de alteração de dados, 1-DataMonitoringUsingDatabase, 2-CDC(Change Data Capture)
        public static int functionDataMonitoring = 1;

        public static System.Timers.Timer _timer = new System.Timers.Timer();
        static async Task Main(string[] args)
        {            
            //criar o singleton dos serviços que irão ser necessários
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IDataService, DataService>()
            .AddSingleton<IContextAcquisitonDb, ContextAcquisitonDb>()
            .AddDbContext<ContextAcquisitonDb>()
            .AddHttpClient()
            .AddSingleton<IHttpClientWrapper, HttpClientWrapper>(provider =>
            {
                var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();

                return new HttpClientWrapper(httpClient);
            })
            .AddSingleton<ILogic>(provider =>
            {               
                var context = provider.GetRequiredService<IContextAcquisitonDb>();
                var dataService = provider.GetRequiredService<IDataService>();
                var httpClientWrapper = provider.GetRequiredService<IHttpClientWrapper>();

                var logic = new Logic(context, dataService, httpClientWrapper);
                return logic;
            })
            .BuildServiceProvider();

            var dataService = serviceProvider.GetRequiredService<IDataService>();
            var _context = serviceProvider.GetRequiredService<ContextAcquisitonDb>();
            var _logicService = serviceProvider.GetRequiredService<ILogic>();

            //inicializar o contexto para haver uma primeira data de pesquisa
            DbInitializer.Initalize(_context);

            //Inicialmente vou começar com um Timer para todas as funções esse timer será de 30 em 30 segundos
            _timer.AutoReset = false;
            _timer.Interval = 30000; // Intervalo em milésimos 30000(30 segundos)

            if(functionDataMonitoring == 1)
            {
                _timer.Elapsed += async (sender, arq) =>
                {
                    if (sender != null)
                    {
                        await DataMonitoringUsingDatabase(sender, arq, dataService, _logicService, _context);
                    }
                };
            }
            if (functionDataMonitoring == 2)
            {
                //adicionar e criar o cdc
                _timer.Elapsed += async (sender, arq) =>
                {
                    if (sender != null)
                    {
                        await ChangeDataCapture(sender, arq, dataService, _logicService, _context);
                    }
                };
            }

            _timer.Enabled = true;
            await Task.Run(() => Thread.Sleep(Timeout.Infinite));
        }

        static async Task DataMonitoringUsingDatabase(object sender, System.Timers.ElapsedEventArgs e, IDataService _dataService, ILogic _logicService, ContextAcquisitonDb _context)
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
                    if(!_context.Stops.Any(s => s.Id.Equals(stop.Id) && s.Planned.Equals(stop.Planned) && s.InitialDate.Equals(stop.InitialDate)
                    && s.EndDate.Equals(stop.EndDate) && s.Duration.Equals(stop.Duration) && s.Shift.Equals(stop.Shift) 
                    && s.LineId.Equals(stop.LineId) && s.ReasonId.Equals(stop.ReasonId)))
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
                    if (!_context.Productions.Any(p => p.Id.Equals(production.Id) && p.Hour.Equals(production.Hour) 
                    && p.Day.Equals(production.Day) && p.Quantity.Equals(production.Quantity)
                    && p.Production_PlanId.Equals(production.Production_PlanId)))
                    {
                        //se não existir foi detetada uma modificação de nova inserção ou update neste stop
                        ITU.productions.Add(production);
                    }
                }
            }
            await _logicService.UpdateItensDMUD(ITU);
            //---------------
            _timer.Enabled = true;
        }

        static async Task ChangeDataCapture(object sender, System.Timers.ElapsedEventArgs e, IDataService _dataService, ILogic _logicService, ContextAcquisitonDb _context)
        {
            _timer.Enabled = false;
            //--------------

            //buscar as datas de ultima verificação
            var lvr = _context.LastVerificationRegists.First();
            if (lvr == null)
            {
                Console.WriteLine("Erro ao inicializar a BD");
                return;
            }
            //datetime verificação atual
            DateTime thisVerification = DateTime.Now;
            //ver as alterações de Stops
            ItensToUpdate ITU = new ItensToUpdate();
            var stops = await _dataService.GetCDC_Stops(thisVerification);
            if(stops != null)
            {
                ITU.Cdc_Stops = stops;
            }
            var productions = await _dataService.GetCDC_Productions(thisVerification);
            if (productions != null)
            {
                ITU.Cdc_Productions = productions;
            }
            //no final manda atualizar os itens
            await _logicService.UpdateItensCDC(ITU, thisVerification);

            //---------------
            _timer.Enabled = true;
        }

    }

}