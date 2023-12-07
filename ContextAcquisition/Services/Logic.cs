using ContextAcquisition.Data;
using Models.ContextModels;
using Models.FunctionModels;
using Services.DataServices;
using System.Net.Http.Json;


namespace ContextAcquisition.Services
{
    public class Logic : ILogic
    { 
        private static readonly string NASHost = System.Environment.GetEnvironmentVariable("NAS") ?? "https://localhost:7013";
        private static readonly string AlertAppConnectionString = $"{NASHost}/api/ServiceLayer/SendNotification/";

        private readonly IContextAcquisitonDb _context;
        private readonly IDataService _dataService;
        public Logic(IContextAcquisitonDb context, IDataService dataService)
        {
            _context = context;
            _dataService = dataService;
        }

        /// <summary>
        /// Esta função tem como finalidade realizar a verificação das alterações nos dados por meio da abordagem de monitorização 
        /// de dados, verificando na base de dados. Para isso, ela receberá um objeto "ItensToUpdate", no qual haverá uma lista de
        /// paragens e uma lista de produções. Com essas informações, a função procederá à atualização dos objetos na base de dados
        /// e, posteriormente, verificará se é necessário enviar avisos.
        /// </summary>
        public async Task UpdateItensDMUD(ItensToUpdate ITU)
        {
            DateTime inicio = DateTime.Now;
            //-----------escrever no ecra o nº de itens para atualizar--------
            Console.WriteLine($"-----------------  {inicio}  ----------------");
            //productions
            Console.WriteLine($"{ITU.productions.Count} Productions novos/atualizados detetados.");
            //stops
            Console.WriteLine($"{ITU.stops.Count} Stops novos/atualizados detetados.");
            //Escrever no fim
            Console.WriteLine("---------------Atualizacao dos itens---------------------------");

            //------------------Depois iserir ou atualizar os dados----------------------
            //stops
            if (ITU.stops != null)
            {
                foreach (var s in ITU.stops)
                {
                    await UpdateStop(s);
                    await CheckIfIsUrgentStop(s);
                }
            }       
            //production
            if (ITU.productions != null)
            {
                foreach (var p in ITU.productions)
                {
                    await UpdateProduction(p);
                    await CheckIfItIsNewProduction(p);
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Código executado em: {DateTime.Now.Subtract(inicio)}.");
        }

        /// <summary>
        /// Esta função tem como finalidade realizar a verificação das alterações nos dados por meio da abordagem 
        /// CDC - Change Data Capture. Para isso, ela receberá um objeto "ItensToUpdate", no qual haverá uma lista 
        /// de cdc_paragens e uma lista de cdc_produções que tirá de passar para o formato correto. Com essas 
        /// informações, a função procederá à verificação se é necessário enviar avisos.
        /// </summary>
        public async Task UpdateItensCDC(ItensToUpdate ITU, DateTime lastVerification)
        {
            DateTime inicio = DateTime.Now;
            //-----------escrever no ecra o nº de itens para atualizar--------
            Console.WriteLine($"-----------------  {inicio}  ----------------");
            //productions
            Console.WriteLine($"{ITU.productions.Count} Productions novos/atualizados detetados.");
            //stops
            Console.WriteLine($"{ITU.stops.Count} Stops novos/atualizados detetados.");
            //Escrever no fim
            Console.WriteLine("---------------Atualizacao dos itens---------------------------");

            //------------------Depois iserir ou atualizar os dados----------------------
            //stops
            if (ITU.Cdc_Stops != null)
            {
                foreach (var cdc_stop in ITU.Cdc_Stops)
                {
                    //só vai fazer a verificação nos updates e insertes
                    if (cdc_stop.Operation == 2 || cdc_stop.Operation == 3)
                    {
                        await CheckIfIsUrgentStop(cdc_stop.toStop());
                    }
                }
            }
            if (ITU.Cdc_Productions != null)
            {
                foreach (var cdc_production in ITU.Cdc_Productions)
                {
                    //só vai fazer a verificação nos updates e insertes
                    if (cdc_production.Operation == 2 || cdc_production.Operation == 3)
                    {
                        await CheckIfItIsNewProduction(cdc_production.ToProduction());
                    }
                }
            }
            //atualizar a data de ultima verificação
            var lvr = _context.LastVerificationRegists.First();
            lvr.LastVerification = lastVerification;
            _context.LastVerificationRegists.Update(lvr);
            await _context.SaveChangesAsync();

            Console.WriteLine();
            Console.WriteLine($"Código executado em: {DateTime.Now.Subtract(inicio)}.");
        }

        /// <summary>
        /// Esta função receberá uma paragem que deverá ser atualizada na base de dados e será responsável por 
        /// adicioná-la caso ainda não exista ou atualizá-la caso já exista na base de dados.
        /// </summary>
        public async Task UpdateStop(Stop stop)
        {
            var stopContext = _context.Stops.SingleOrDefault(s => s.Id == stop.Id);
            if (stopContext == null)
            {
                try
                {
                    _context.Add(stop);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Stop: ID-{stop.Id} Adicionado com suceso.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exceção:{e.Message}");
                }
            }
            else
            {
                //fazer update
                try
                {
                    stopContext.ReasonId = stop.ReasonId;
                    stopContext.LineId = stop.LineId;
                    stopContext.Planned = stop.Planned;
                    stopContext.InitialDate = stop.InitialDate;
                    stopContext.EndDate = stop.EndDate;
                    stopContext.Duration = stop.Duration;
                    stopContext.Shift = stop.Shift;
                    _context.Update(stopContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Stop: ID-{stop.Id} Atualizado com suceso.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exceção:{e.Message}");
                }
            }
        }

        /// <summary>
        /// Esta função receberá uma produção que deverá ser atualizada na base de dados e será responsável por 
        /// adicioná-la caso ainda não exista ou atualizá-la caso já exista na base de dados.
        /// </summary>
        public async Task UpdateProduction(Production production)
        {
            var productionContext = _context.Productions.SingleOrDefault(p => p.Id == production.Id);
            if (productionContext == null)
            {
                try
                {
                    _context.Add(production);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Production: ID-{production.Id} Adicionado com suceso");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exceção:{e.Message}");
                }
            }
            else
            {
                //fazer update
                try
                {
                    productionContext.Production_PlanId = production.Production_PlanId;
                    productionContext.Hour = production.Hour;
                    productionContext.Day = production.Day;
                    productionContext.Quantity = production.Quantity;
                    _context.Update(productionContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Production: ID-{production.Id} Atualizada com suceso.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exceção:{e.Message}");
                }
            }
        }

        /// <summary>
        /// Está função é responsável por verificar se a paragem é considerada uma paragem urgente. Caso seja 
        /// chamará a função para enviar o alerta.
        /// </summary>
        public async Task CheckIfIsUrgentStop(Stop stop)
        {
            //ver se a paragem durou mais de 15 min, se não é planeada e se foi no dia de hoje
            if (stop.Duration.TotalMinutes < 15 || stop.Planned == true || !stop.InitialDate.Date.Equals(DateTime.Now.Date))
            {
                Console.WriteLine($"Stop: ID-{stop.Id} não é urgente.");
                return;
            }
            var _sendeAlertRequest = new SendAlertRequest
            {
                Stop = stop,
            };
            if (stop.ReasonId != null)
            {
                var reason = await _dataService.GetReasonById((int)stop.ReasonId);
                if (reason != null)
                {
                    _sendeAlertRequest.Message = $"Paragem urgente detetada, Id-{stop.Id}, LineId-{stop.LineId}, Razão - {reason.Description}.";
                }
            }
            //Soar o alerta
            await SendAlert(_sendeAlertRequest, 1);
        }

        /// <summary>
        /// Está função é responsável por verificar se a produção é considerada importante. Caso seja 
        /// chamará a função para enviar o alerta.
        /// </summary>
        public async Task CheckIfItIsNewProduction(Production production)
        {
            //A produção será considerada importante para o envio de alerta se ocorreu nas ultimas 24 horas.
            DateTime ProductionDateTime = production.Day.Date.AddHours(production.Hour);
            TimeSpan ts = DateTime.Now.Subtract(ProductionDateTime);
            if (ts.TotalHours > 24)
            {
                Console.WriteLine($"Production: ID-{production.Id} não é das ultimas 24 horas, não enviar notificação.");
                return;
            }
            //Soar o aviso
            var _sendeAlertRequest = new SendAlertRequest
            {
                Production = production,
                Message = $"Foi detetada uma nova produção nas ultimas 24 horas, Id-{production.Id}, Quantidade-{production.Quantity}, ProductionPlan-{production.Production_PlanId}.",
            };
            //Soar o alerta
            await SendAlert(_sendeAlertRequest, 2);
        }


        /// <summary>
        /// Esta função receberá um alerta para enviar e o tipo de alerta (1 - paragem, 2 - produção). Com esses dados, a função 
        /// enviará o alerta para o NAS e registrará na base de dados do Context Engine se o alerta foi enviado com sucesso ou não.
        /// </summary>
        public async Task SendAlert(SendAlertRequest _sendAlertRequest, int TypeOfAlert)
        {
            if (TypeOfAlert == 1 && _sendAlertRequest.Stop == null)
            {
                Console.WriteLine("Erro de código na parte do enviar alertas, se o tipo de alerta for 1, tem de ter um stop na mensagem!!");
                return;
            }
            if (TypeOfAlert == 2 && _sendAlertRequest.Production == null)
            {
                Console.WriteLine("Erro de código na parte do enviar alertas, se o tipo de alerta for 2, tem de ter uma paragem na mensagem!!");
                return;
            }
            var alertHistory = new AlertsHistory
            {
                TypeOfAlet = TypeOfAlert,
                AlertDate = DateTime.Now,
                AlertMessage = _sendAlertRequest.Message
            };
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(AlertAppConnectionString, _sendAlertRequest);
                    response.EnsureSuccessStatusCode();
                    //Se passar para baixo é pq foi enviado com sucesso
                    alertHistory.AlertSuccessfullySent = true;
                    //Alerta de paragem
                    if (TypeOfAlert == 1)
                    {                       
                        Console.WriteLine($"Alerta da Paragem: ID-{_sendAlertRequest.Stop?.Id} Enviado com sucesso.");
                    }
                    if (TypeOfAlert == 2)
                    {
                        Console.WriteLine($"Alerta da Produção: ID-{_sendAlertRequest.Production?.Id} Enviado com sucesso.");
                    }
                }
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (TypeOfAlert == 1)
                {
                    Console.WriteLine($"Erro ao enviar alerta de Paragem: ID-{_sendAlertRequest.Stop?.Id}.");
                }
                if (TypeOfAlert == 2)
                {
                    Console.WriteLine($"Erro ao enviar alerta de Produção: ID-{_sendAlertRequest.Production?.Id}.");
                }
                Console.WriteLine($"Exceção: {ex.Message}");
                alertHistory.ErrorMessage = ex.Message;
                alertHistory.AlertSuccessfullySent = false;
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
