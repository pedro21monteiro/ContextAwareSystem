using ContextAcquisition.Data;
using Models.ContextModels;
using Models.FunctionModels;
using Newtonsoft.Json;
using Services.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public class Logic : ILogic
    {
        //private static string connectionString = "https://localhost:7284/api/ContextBuilder/";
        //private static string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
        private static string AlertAppConnectionString = "https://localhost:7013/api/ServiceLayer/SendNotification/";
        //private static string builderHost = System.Environment.GetEnvironmentVariable("BUILDER") ?? "https://localhost:7284";
        private static readonly ContextAcquisitonDb _context = new ContextAcquisitonDb();
        private static readonly IDataService _dataService = new DataService();

        //Update dos itens todos
        public async Task UpdateItensDMUD(ItensToUpdate ITU)
        {
            DateTime inicio = DateTime.Now;
            //-----------escrever no ecra o nº de itens para atualizar--------
            Console.WriteLine("-----------------  " + inicio.ToString() + "  ----------------");
            
            //productions
            if (ITU.productions != null)
            {
                Console.WriteLine(ITU.productions.Count.ToString() + " productions novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 productions novos/atualizados detetados");
            }
           
            //stops
            if (ITU.stops != null)
            {
                Console.WriteLine(ITU.stops.Count.ToString() + " stops novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 stops novos/atualizados detetados");
            }
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
            TimeSpan tempoDeExecucao = DateTime.Now.Subtract(inicio);
            Console.WriteLine("Código executado em: " + tempoDeExecucao.ToString());

        }

        //-------------------------
        //Os Updates todos
        
        public static async Task UpdateStop(Stop stop)
        {
            var sExistInContext = _context.Stops.SingleOrDefault(s => s.Id == stop.Id);
            if (sExistInContext == null)
            {
                try
                {
                    _context.Add(stop);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + stop.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                //fazer update
                try
                {
                    if (stop.ReasonId != null)
                    {
                        sExistInContext.ReasonId = stop.ReasonId;
                    }
                    sExistInContext.LineId = stop.LineId;
                    //o resto do stop
                    sExistInContext.Planned = stop.Planned;
                    sExistInContext.InitialDate = stop.InitialDate;
                    sExistInContext.EndDate = stop.EndDate;
                    sExistInContext.Duration = stop.Duration;
                    sExistInContext.Shift = stop.Shift;
                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + stop.Id.ToString() + " - atualizado com suceso");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        public static async Task UpdateProduction(Production production)
        {
            var pExistInContext = _context.Productions.SingleOrDefault(p => p.Id == production.Id);
            if (pExistInContext == null)
            {
                try
                {
                    _context.Add(production);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Production: " + production.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Production_PlanId = production.Production_PlanId;
                    pExistInContext.Hour = production.Hour;
                    pExistInContext.Day = production.Day;
                    pExistInContext.Quantity = production.Quantity;
                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Production: " + production.Id.ToString() + " - Atualizada com suceso");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        //Implementação a usar o CDC
        public async Task UpdateItensCDC(ItensToUpdate ITU, DateTime lastVerification)
        {
            DateTime inicio = DateTime.Now;
            //-----------escrever no ecra o nº de itens para atualizar--------
            Console.WriteLine("-----------------  " + inicio.ToString() + "  ----------------");

            //productions
            if (ITU.productions != null)
            {
                Console.WriteLine(ITU.Cdc_Productions.Count.ToString() + " productions novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 productions novos/atualizados detetados");
            }

            //stops
            if (ITU.stops != null)
            {
                Console.WriteLine(ITU.Cdc_Stops.Count.ToString() + " stops novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 stops novos/atualizados detetados");
            }
            //Escrever no fim
            Console.WriteLine("---------------Atualizacao dos itens---------------------------");

            //------------------Depois iserir ou atualizar os dados----------------------
            //stops
            if (ITU.Cdc_Stops != null)
            {
                foreach (var cdc_stop in ITU.Cdc_Stops)
                {
                    //só vai fazer a verificação nos updates e insertes
                    if(cdc_stop.Operation == 2 || cdc_stop.Operation == 3)
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
            //
            Console.WriteLine();
            TimeSpan tempoDeExecucao = DateTime.Now.Subtract(inicio);
            Console.WriteLine("Código executado em: " + tempoDeExecucao.ToString());

        }

        //Funções para ver se é para adicionar avisos
        public static async Task CheckIfIsUrgentStop(Stop stop)
        {
            //ver se a paragem durou mais de 15 min, se não é planeada e se foi no dia de hoje
            if (stop.Duration.TotalMinutes < 15 || stop.Planned == true || !stop.InitialDate.Date.Equals(DateTime.Now.Date))
            {
                Console.WriteLine("Paragem - " + stop.Id.ToString() + " não é urgente");
                return;
            }
            //a paragem é urgente
            //Soar o aviso
            var asr = new SendAlertRequest
            {
                Stop = stop,
            };

            if (stop.ReasonId != null)
            {
                var reason = await _dataService.GetReasonById((int)stop.ReasonId);
                if (reason != null)
                {
                    asr.Message = "Paragem urgente detetada, Id-" + stop.Id + ", LineId-" + stop.LineId + " Razão - " + reason.Description;
                }
            }
            var alertHistory = new AlertsHistory
            {
                TypeOfAlet = 1,
                AlertDate = DateTime.Now,
                AlertMessage = asr.Message
            };
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(AlertAppConnectionString, asr);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Enviado com sucesso");
                        //guardar alerta na bd
                        alertHistory.AlertSuccessfullySent = true;
                    }
                    else
                    {
                        Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
                        //guardar alerta na bd
                        alertHistory.AlertSuccessfullySent = false;
                    }
                }
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                alertHistory.ErrorMessage = e.Message;
                alertHistory.AlertSuccessfullySent = false;
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
                Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
                Console.WriteLine(e.Message);
            }             
        }

        //vai verificar se a produção aconteceu nas ultimas 24 horas
        public static async Task CheckIfItIsNewProduction(Production production)
        {
            DateTime ProductionDateTime = production.Day.Date;
            ProductionDateTime.AddHours(production.Hour);
            TimeSpan ts = DateTime.Now.Subtract(ProductionDateTime);
            if (ts.TotalHours >= 24)
            {
                Console.WriteLine("Production - " + production.Id.ToString() + " não é das ultimas 24 horas");
                return;
            }
            //Soar o aviso
            var asr = new SendAlertRequest
            {
                Production = production,
                Message = "Foi detetada uma nova produção, Id-" + production.Id  + ", Quantidade-" + production.Quantity + ", ProductionPlan- " + production.Production_PlanId,
            };
            var alertHistory = new AlertsHistory
            {
                TypeOfAlet = 2,
                AlertDate = DateTime.Now,
                AlertMessage = asr.Message
            };
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(AlertAppConnectionString, asr);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Alerta de nova production - " + production.Id.ToString() + " Enviado com sucesso");
                        alertHistory.AlertSuccessfullySent = true;
                    }
                    else
                    {
                        Console.WriteLine("Alerta de nova production - " + production.Id.ToString() + " Erro ao enviar Alerta");
                        alertHistory.AlertSuccessfullySent = false;
                    }
                    _context.Add(alertHistory);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                alertHistory.ErrorMessage = e.Message;
                alertHistory.AlertSuccessfullySent = false;
                _context.Add(alertHistory);
                await _context.SaveChangesAsync();
                Console.WriteLine("Alerta de nova production - " + production.Id.ToString() + " Erro ao enviar Alerta");
                Console.WriteLine(e.Message);
            }
        }
    }
}
