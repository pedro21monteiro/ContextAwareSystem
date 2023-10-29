using ContextAcquisition.Data;
using Models.ContextModels;
using Models.JsonModels;
using Newtonsoft.Json;
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

        //Update dos itens todos
        public async Task UpdateItens(ItensToUpdate ITU, DateTime? lastVerification)
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
                //enviar para o messagem broker
                foreach (var s in ITU.stops)
                {
                    await UpdateStop(s);
                }
            }       
            //production
            if (ITU.productions != null)
            {
                //enviar para o messagem broker
                foreach (var p in ITU.productions)
                {
                    await UpdateProduction(p);
                }
            }
            if(lastVerification.HasValue)
            {
                var lvr = _context.LastVerificationRegists.First();
                lvr.ProductionsVerification = (DateTime)lastVerification;
                lvr.StopsVerification = (DateTime)lastVerification;
                _context.LastVerificationRegists.Update(lvr);
                await _context.SaveChangesAsync();
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
        

        ////Funções de ver se existem situações cíticas
        //public static async Task CheckIfIsUrgentStop(Stop stop, ContextAcquisitonDb _context)
        //{
        //    try
        //    {
        //        TimeSpan ts = stop.EndDate.Subtract(stop.InitialDate);
        //        if (ts.TotalMinutes >= UrgentStopTime)
        //        {
        //            //soar o aviso
        //            AlertRequest ar = new AlertRequest();
        //            ar.type = 0;//alerta de paragens
        //            ar.line = stop.LineId;
        //            ar.shift = stop.Shift;
        //            ar.dateStart = stop.InitialDate;
        //            ar.dateEnd = stop.EndDate;
        //            if (stop.ReasonId != null)
        //            {
        //                var reason = _context.Reasons.First(r => r.Id == stop.ReasonId);
        //                if (reason != null)
        //                {
        //                    ar.message = reason.Description;
        //                }
        //            }

        //            HttpClient client = new HttpClient();
        //            HttpResponseMessage response = await client.PostAsJsonAsync(AlertAppConnectionString, ar);
        //            response.EnsureSuccessStatusCode();

        //            if (response.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Enviado com sucesso");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
        //            }
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
        //    }
        //}


    }
}
