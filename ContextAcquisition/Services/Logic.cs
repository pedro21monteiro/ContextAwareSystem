using ContextAcquisition.Data;
using Models.ContextModels;
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
        private static string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
        private static int UrgentStopTime = 15;
        private static string AlertAppConnectionString = "https://192.168.28.86:8091/api/Alert/SendNotification/";
        private static string builderHost = System.Environment.GetEnvironmentVariable("BUILDER") ?? "https://localhost:7284";
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
            try
            {
                HttpClient _httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(stop);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{builderHost}/api/DataChange/ChangeStop", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Stop: " + stop.Id.ToString() + " - Atualizado com suceso");
                }
                else
                {
                    Console.WriteLine("Erro ao atualizar Stop: " + stop.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        public static async Task UpdateProduction(Production production)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(production);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{builderHost}/api/DataChange/ChangeProduction", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Production: " + production.Id.ToString() + " - Atualizado com suceso");
                }
                else
                {
                    Console.WriteLine("Erro ao atualizar Production: " + production.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
            try
            {
                TimeSpan ts = stop.EndDate.Subtract(stop.InitialDate);
                //ver se a paragem durou mais de 15 min, se não é planeada e se foi no dia de hoje
                if (ts.TotalMinutes >= UrgentStopTime && stop.Planned == false && stop.InitialDate.Date.Equals(DateTime.Now.Date))
                {
                    //Soar o aviso
                    var asr = new AlertStopRequest
                    {
                        lineId = stop.LineId,
                        shift = stop.Shift,
                        dateStart = stop.InitialDate,
                        dateEnd = stop.EndDate
                    };

                    if (stop.ReasonId != null)
                    {
                        var reason = await _dataService.GetReasonById((int)stop.ReasonId);
                        if (reason != null)
                        {
                            asr.message = reason.Description;
                        }
                    }
                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsJsonAsync(AlertAppConnectionString, asr);
                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Enviado com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Paragem - " + stop.Id.ToString() + " não é urgente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
                Console.WriteLine(ex.ToString());
            }           
        }

        //vai verificar se a produção aconteceu nas ultimas 24 horas
        public static async Task CheckIfItIsNewProduction(Production production)
        {
            DateTime ProductionDateTime = new DateTime();
            ProductionDateTime = production.Day.Date;
            ProductionDateTime.AddHours(production.Hour);
            try
            {
                TimeSpan ts = DateTime.Now.Subtract(ProductionDateTime);
                if (ts.TotalHours < 24)
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsJsonAsync(AlertAppConnectionString, production);
                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Alerta de nova production - " + production.Id.ToString() + " Enviado com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Alerta de nova production - " + production.Id.ToString() + " Erro ao enviar Alerta");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Production - " + production.Id.ToString() + " não é das ultimas 24 horas");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta de nova production - " + production.Id.ToString() + " Erro ao enviar Alerta");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
