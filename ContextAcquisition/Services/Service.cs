using ContextAcquisition.Data;
using Models.ContextModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public class Service : IService
    {
        private readonly HttpClient httpClient;
        private static string connectionString = "https://localhost:7284/api/ContextBuilder/";
        private static string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
        private static int UrgentStopTime = 15;
        private static string AlertAppConnectionString = "https://192.168.28.86:8091/api/Alert/SendNotification/";
        public Service(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        //Lista de serviços que podem ser pedidos à camada de integração com as bases de dados

        //EX: https://localhost:7013/api/ContinentalAPI/GetComponents?InicialDate=2020-05-05

        //Componentes
        public async Task<List<Component>> GetComponents(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                return await httpClient.GetFromJsonAsync<List<Component>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponents");

                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Component>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponents/" + "?InicialDate=" + ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                    
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }
        //Coordinadores
        public async Task<List<Coordinator>> GetCoordinators(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Coordinator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetCoordinators");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Coordinator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetCoordinators/" + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        //Devices
        public async Task<List<Device>> GetDevices(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Device>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetDevices");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Device>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetDevices/" + "?InicialDate=" + ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        //Lines
        public async Task<List<Line>> GetLines(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetLines");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetLines/" + "?InicialDate=" + ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }
        //Operators
        public async Task<List<Operator>> GetOperators(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Operator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetOperators");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Operator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetOperators/" + "?InicialDate=" + ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Product>> GetProducts(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Product>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProducts");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Product>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProducts/" + "?InicialDate=" + ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Production>> GetProductions(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Production>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProductions");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Production>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProductions/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Production_Plan>> GetProductionPlans(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Production_Plan>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProductionPlans");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Production_Plan>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProductionPlans/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Reason>> GetReasons(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Reason>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetReasons");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Reason>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetReasons/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Request>> GetRequests(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Request>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetRequests");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Request>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetRequest/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Schedule_Worker_Line>> GetSchedule_Worker_Lines(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSchedule_Worker_Lines");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSchedule_Worker_Lines/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Stop>> GetStops(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Stop>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetStops");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Stop>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetStops/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Supervisor>> GetSupervisors(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Supervisor>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSupervisors");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Supervisor>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSupervisors/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<Worker>> GetWorkers(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<Worker>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetWorkers");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<Worker>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetWorkers/"  + "?InicialDate=" +  ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }

        public async Task<List<ComponentProduct>> GetComponentProducts(DateTime? DataInicial)
        {
            if (DataInicial == null)
            {
                try
                {
                    return await httpClient.GetFromJsonAsync<List<ComponentProduct>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponentProducts");
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }
            else
            {
                try
                {
                    //GetComponents?InicialDate=2020-05-05
                    return await httpClient.GetFromJsonAsync<List<ComponentProduct>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponentProducts/" + "?InicialDate=" + ((DateTime)DataInicial).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }


        //Update dos itens todos
        public static async Task UpdateItens(ItensToUpdate ITU, ContextAcquisitonDb _context)
        {
            DateTime inicio = DateTime.Now;
            //-----------escrever no ecra o nº de itens para atualizar--------
            Console.WriteLine("-----------------  " + inicio.ToString() + "  ----------------");
            //componentes
            if (ITU.components != null)
            {
                Console.WriteLine(ITU.components.Count.ToString() + " componentes novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 componentes novos/atualizados detetados");
            }
            //coordinator
            if (ITU.components != null)
            {
                Console.WriteLine(ITU.coordinators.Count.ToString() + " coordinators novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 coordinators novos/atualizados detetados");
            }
            //devices
            if (ITU.devices != null)
            {
                Console.WriteLine(ITU.devices.Count.ToString() + " devices novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 devices novos/atualizados detetados");
            }
            //lines
            if (ITU.lines != null)
            {
                Console.WriteLine(ITU.lines.Count.ToString() + " lines novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 lines novos/atualizados detetados");
            }
            //operators
            if (ITU.components != null)
            {
                Console.WriteLine(ITU.operators.Count.ToString() + " operators novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 operators novos/atualizados detetados");
            }
            //Products
            if (ITU.products != null)
            {
                Console.WriteLine(ITU.products.Count.ToString() + " products novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 products novos/atualizados detetados");
            }
            //productions
            if (ITU.productions != null)
            {
                Console.WriteLine(ITU.productions.Count.ToString() + " productions novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 productions novos/atualizados detetados");
            }
            //ProductionPlans
            if (ITU.production_Plans != null)
            {
                Console.WriteLine(ITU.production_Plans.Count.ToString() + " production_Plans novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 productionPlans novos/atualizados detetados");
            }
            //reasons
            if (ITU.reasons != null)
            {
                Console.WriteLine(ITU.reasons.Count.ToString() + " reasons novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 reasons novos/atualizados detetados");
            }
            //if (ITU.requests != null)
            //{
            //    Console.WriteLine(ITU.requests.Count.ToString() + " requests novos/atualizados detetados");
            //}
            //else
            //{
            //    Console.WriteLine("0 requests novos/atualizados detetados");
            //}
            //schedules
            if (ITU.schedules != null)
            {
                Console.WriteLine(ITU.schedules.Count.ToString() + " schedules novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 schedures novos/atualizados detetados");
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
            //supervisors
            if (ITU.supervisors != null)
            {
                Console.WriteLine(ITU.supervisors.Count.ToString() + " supervisors novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 supervisors novos/atualizados detetados");
            }
            //Workers
            if (ITU.workers != null)
            {
                Console.WriteLine(ITU.workers.Count.ToString() + " workers novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 workers novos/atualizados detetados");
            }
            //ComponentProducts
            if (ITU.ComponentProducts != null)
            {
                Console.WriteLine(ITU.ComponentProducts.Count.ToString() + " ComponentProducts novos/atualizados detetados");
            }
            else
            {
                Console.WriteLine("0 ComponentProducts novos/atualizados detetados");
            }
            //EScrever o fim
            Console.WriteLine("---------------Atualizacao dos itens---------------------------");

            //------------------Depois iserir os dados----------------------
            //componentes
            if (ITU.components != null)
            {
                //enviar para o messagem broker
                foreach (var comp in ITU.components)
                {
                    await UpdateComponent(comp, _context);
                }
            }
            //reasons
            if (ITU.reasons != null)
            {
                //enviar para o messagem broker
                foreach (var r in ITU.reasons)
                {
                    await UpdateReason(r, _context);
                }
            }
            //workers
            if (ITU.workers != null)
            {
                //enviar para o messagem broker
                foreach (var w in ITU.workers)
                {
                    await UpdateWorker(w, _context);
                }
            }
            //coordinators
            if (ITU.coordinators != null)
            {
                //enviar para o messagem broker
                foreach (var coordinator in ITU.coordinators)
                {
                    await UpdateCoordinator(coordinator, _context);                    
                }
            }
            //supervisors
            if (ITU.supervisors != null)
            {
                //enviar para o messagem broker
                foreach (var s in ITU.supervisors)
                {
                    await UpdateSupervisor(s, _context);
                }
            }
            //Operators
            if (ITU.operators != null)
            {
                //enviar para o messagem broker
                foreach (var o in ITU.operators)
                {
                    await UpdateOperator(o, _context);
                }
            }
            //requests
            //if (ITU.requests != null)
            //{
            //    //enviar para o messagem broker
            //    foreach (var r in ITU.requests)
            //    {
            //        await UpdateRequest(r, _context);
            //    }
            //}
            //lines
            if (ITU.lines != null)
            {
                //enviar para o messagem broker
                foreach (var l in ITU.lines)
                {
                    await UpdateLine(l, _context);
                }
            }
            //stops
            if (ITU.stops != null)
            {
                //enviar para o messagem broker
                foreach (var s in ITU.stops)
                {
                    await UpdateStop(s, _context);
                }
            }
            //stops
            if (ITU.products != null)
            {
                //enviar para o messagem broker
                foreach (var p in ITU.products)
                {
                    await UpdateProduct(p, _context);
                }
            }
            //devices
            if (ITU.devices != null)
            {
                //enviar para o messagem broker
                foreach (var d in ITU.devices)
                {
                    await UpdateDevice(d, _context);
                }
            }
            //productionPlans
            if (ITU.production_Plans != null)
            {
                //enviar para o messagem broker
                foreach (var p in ITU.production_Plans)
                {
                    await UpdateProductionPlan(p, _context);
                }
            }
            //production
            if (ITU.productions != null)
            {
                //enviar para o messagem broker
                foreach (var p in ITU.productions)
                {
                    await UpdateProduction(p, _context);
                }
            }
            //Schedules
            if (ITU.schedules != null)
            {
                //enviar para o messagem broker
                foreach (var s in ITU.schedules)
                {
                    await UpdateSchedules(s, _context);
                }
            }
            //ComponentProducts
            if (ITU.ComponentProducts != null)
            {
                //enviar para o messagem broker
                foreach (var cp in ITU.ComponentProducts)
                {
                    await UpdateComponentProducts(cp, _context);
                }
            }

            //no fim
            //meter as classes todas com a data da ultima vizualização
            var lvr = _context.LastVerificationRegists.First();
            DateTime lastverification = DateTime.Now;
            lvr.ComponentsVerification = lastverification;
            lvr.CoordinatorsVerification = lastverification;
            lvr.DevicesVerification = lastverification;
            lvr.LinesVerification = lastverification;
            lvr.OperatorsVerification = lastverification;
            lvr.ProductsVerification = lastverification;
            lvr.ProductionsVerification = lastverification;
            lvr.ProductionPlansVerification = lastverification;
            lvr.ReasonsVerification = lastverification;
            lvr.RequestsVerification = lastverification;
            lvr.Schedule_worker_linesVerification = lastverification;
            lvr.StopsVerification = lastverification;
            lvr.SupervisorsVerification = lastverification;
            lvr.WorkersVerification = lastverification;
            lvr.ComponentProductsVerification = lastverification;

            //falta meter o resto
            _context.LastVerificationRegists.Update(lvr);
            await _context.SaveChangesAsync();

            Console.WriteLine();
            TimeSpan tempoDeExecucao = DateTime.Now.Subtract(inicio);
            Console.WriteLine("Código executado em: " + tempoDeExecucao.ToString());

        }

        //-------------------------
        //Os Updates todos

        public static async Task UpdateComponent(Component component, ContextAcquisitonDb _context)
        {
            var cExistInContext = _context.Components.SingleOrDefault(c => c.Id == component.Id);
            if (cExistInContext == null)
            {
                //Fazer Create
                try
                {
                    _context.Add(component);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Componente: " + component.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    cExistInContext.Name = component.Name;
                    cExistInContext.Reference = component.Reference;
                    cExistInContext.Category = component.Category;
                    cExistInContext.LastUpdate = component.LastUpdate;

                    _context.Update(cExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Componente: " + cExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }


        }

        public static async Task UpdateCoordinator(Coordinator coordinator, ContextAcquisitonDb _context)
        {
            //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
            //neste caso o worker
            var workercontext = _context.Workers.SingleOrDefault(w => w.Id == coordinator.WorkerId);
            if (workercontext == null)
            {
                //vai criar o worker e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateWorker(coordinator.Worker, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateCoordinator(coordinator, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //agora só tem de fazer o update/add do coordinator pois apartir daqui o worker já existe
            var cExistInContext = _context.Coordinators.SingleOrDefault(c => c.Id == coordinator.Id);
            if (cExistInContext == null)
            {
                try
                {
                    coordinator.Worker = workercontext;
                    coordinator.WorkerId = workercontext.Id;
                    _context.Add(coordinator);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Coordinator: " + coordinator.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    cExistInContext.Worker = workercontext;
                    cExistInContext.WorkerId = workercontext.Id;
                    cExistInContext.LastUpdate = coordinator.LastUpdate;

                    _context.Update(cExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("coordinator: " + cExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task UpdateSupervisor(Supervisor supervisor, ContextAcquisitonDb _context)
        {
            //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
            //neste caso o worker
            var workercontext = _context.Workers.SingleOrDefault(s => s.Id == supervisor.WorkerId);
            if (workercontext == null)
            {
                //vai criar o worker e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateWorker(supervisor.Worker, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateSupervisor(supervisor, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //---------------------
            //agora só tem de fazer o update do coordinator pois apartir daqui o worker já existe
            var sExistInContext = _context.Supervisors.SingleOrDefault(s => s.Id == supervisor.Id);
            if (sExistInContext == null)
            {
                try
                {
                    supervisor.Worker = workercontext;
                    supervisor.WorkerId = workercontext.Id;
                    _context.Add(supervisor);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("supervisor: " + supervisor.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    sExistInContext.Worker = workercontext;
                    sExistInContext.WorkerId = workercontext.Id;
                    sExistInContext.LastUpdate = supervisor.LastUpdate;

                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("supervisor: " + sExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task UpdateOperator(Operator ope, ContextAcquisitonDb _context)
        {
            //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
            //neste caso o worker
            var workercontext = _context.Workers.SingleOrDefault(s => s.Id == ope.WorkerId);
            if (workercontext == null)
            {
                //vai criar o worker e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateWorker(ope.Worker, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateOperator(ope, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //---------------------
            //agora só tem de fazer o update do coordinator pois apartir daqui o worker já existe
            var oExistInContext = _context.Operators.SingleOrDefault(s => s.Id == ope.Id);
            if (oExistInContext == null)
            {
                try
                {
                    ope.Worker = workercontext;
                    ope.WorkerId = workercontext.Id;
                    _context.Add(ope);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("operator: " + ope.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    oExistInContext.Worker = workercontext;
                    oExistInContext.WorkerId = workercontext.Id;
                    oExistInContext.LastUpdate = ope.LastUpdate;

                    _context.Update(oExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("operator: " + oExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        //public static async Task UpdateRequest(Request request, ContextAcquisitonDb _context)
        //{
        //    //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
        //    //neste caso o worker
        //    var workercontext = _context.Workers.SingleOrDefault(s => s.Id == request.WorkerId);
        //    if (workercontext == null)
        //    {
        //        //vai criar o worker e depois mandar fazer esta função de novo
        //        try
        //        {
        //            //vai criar o worker
        //            await UpdateWorker(request.Worker, _context);
        //            //depois de dar o update do worker retira da lista pois já foi atualizado
        //            await UpdateRequest(request, _context);
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }
        //    //---------------------
        //    //agora só tem de fazer o update do coordinator pois apartir daqui o worker já existe
        //    else
        //    {
        //        var rExistInContext = _context.Requests.SingleOrDefault(r => r.Id == request.Id);
        //        if (rExistInContext == null)
        //        {
        //            try
        //            {
        //                request.Worker = workercontext;
        //                request.WorkerId = workercontext.Id;
        //                _context.Add(request);
        //                await _context.SaveChangesAsync();
        //                Console.WriteLine("request: " + request.Id.ToString() + " - Adicionado com suceso");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.ToString());
        //            }
        //        }
        //        else
        //        {
        //            //fazer update
        //            try
        //            {
        //                rExistInContext.Worker = workercontext;
        //                rExistInContext.WorkerId = workercontext.Id;
        //                //aqui já vai buscar ao request
        //                rExistInContext.LastUpdate = request.LastUpdate;
        //                rExistInContext.Type = request.Type;
        //                rExistInContext.Date = request.Date;

        //                _context.Update(rExistInContext);
        //                await _context.SaveChangesAsync();
        //                Console.WriteLine("request: " + rExistInContext.Id.ToString() + " - Atualizado com suceso");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.ToString());
        //            }
        //        }
        //    }
        //}

        //public static async Task UpdateLine(Line line, ContextAcquisitonDb _context)
        //{
        //    //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
        //    //neste caso o worker
        //    var coordinatorcontext = _context.Coordinators.SingleOrDefault(c => c.Id == line.CoordinatorId);
        //    if (coordinatorcontext == null)
        //    {
        //        //vai criar o worker e depois mandar fazer esta função de novo
        //        try
        //        {
        //            //vai criar o worker
        //            await UpdateCoordinator(line.Coordinator, _context);
        //            //depois de dar o update do worker retira da lista pois já foi atualizado
        //            await UpdateLine(line, _context);
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }
        //    //---------------------
        //    //agora só tem de fazer o update da line pois apartir daqui o coordinador já existe
        //    var lExistInContext = _context.Lines.SingleOrDefault(l => l.Id == line.Id);
        //    if (lExistInContext == null)
        //    {
        //        try
        //        {
        //            line.Coordinator = coordinatorcontext;
        //            line.CoordinatorId = coordinatorcontext.Id;
        //            _context.Add(line);
        //            await _context.SaveChangesAsync();
        //            Console.WriteLine("line: " + line.Id.ToString() + " - Adicionado com suceso");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        //fazer update
        //        try
        //        {
        //            lExistInContext.Coordinator = coordinatorcontext;
        //            lExistInContext.CoordinatorId = coordinatorcontext.Id;
        //            //aqui já vai buscar ao line
        //            lExistInContext.LastUpdate = line.LastUpdate;
        //            lExistInContext.Name = line.Name;
        //            lExistInContext.Priority = line.Priority;

        //            _context.Update(lExistInContext);
        //            await _context.SaveChangesAsync();
        //            Console.WriteLine("line: " + lExistInContext.Id.ToString() + " - Atualizado com suceso");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }
        //}

        public static async Task UpdateLine(Line line, ContextAcquisitonDb _context)
        {

            var lExistInContext = _context.Lines.SingleOrDefault(l => l.Id == line.Id);
            if (lExistInContext == null)
            {
                try
                {

                    line.CoordinatorId = line.CoordinatorId;
                    _context.Add(line);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("line: " + line.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    lExistInContext.CoordinatorId = line.CoordinatorId;
                    //aqui já vai buscar ao line
                    lExistInContext.LastUpdate = line.LastUpdate;
                    lExistInContext.Name = line.Name;
                    lExistInContext.Priority = line.Priority;

                    _context.Update(lExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("line: " + lExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task UpdateProduct(Product product, ContextAcquisitonDb _context)
        {
            //////Em primeiro lugar vai ver se as outras classes, o product só tem uma lista de componentes, vamos ter de ver se todos já estão criados
            //if (product.Components.Any())
            //{
            //    foreach (var comp in product.Components)
            //    {
            //        var component = _context.Components.SingleOrDefault(c => c.Id == comp.Id);
            //        if (component == null)
            //        {
            //            //vai criar o component e depois mandar fazer esta função de novo
            //            try
            //            {
            //                //vai criar o component
            //                await UpdateComponent(comp, _context);
            //                //depois de dar o update do worker retira da lista pois já foi atualizado
            //                await UpdateProduct(product, _context);
            //                return;
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.ToString());
            //            }
            //        }
            //    }
            //}
            ////---------------------
            ////agora só tem de fazer o update do product pois apartir daqui o coordinador já existe
            //var pExistInContext = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            //if (pExistInContext == null)
            //{
            //    //create product
            //    try
            //    {
            //        List<Component> listComponents = new List<Component>();
            //        if (product.Components.Any())
            //        {
            //            foreach (var comp1 in product.Components)
            //            {
            //                listComponents.Add(comp1);
            //            }
            //            product.Components.Clear();

            //            foreach (var comp2 in listComponents)
            //            {
            //                var c = _context.Components.SingleOrDefault(c => c.Id == comp2.Id);
            //                if (c != null)
            //                {
            //                    product.Components.Add(c);
            //                }
            //            }
            //        }
            //        _context.Add(product);
            //        await _context.SaveChangesAsync();
            //        Console.WriteLine("product: " + product.Id.ToString() + " - Adicionado com suceso");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }
            //}
            //else
            //{
            //    //fazer update
            //    try
            //    {

            //        pExistInContext.Components.Clear();
            //        foreach (var comp3 in product.Components)
            //        {
            //            var co = _context.Components.SingleOrDefault(c => c.Id == comp3.Id);
            //            if (co != null)
            //            {
            //                pExistInContext.Components.Add(co);
            //            }
            //        }
            //        //resto
            //        pExistInContext.Name = product.Name;
            //        pExistInContext.LabelReference = product.LabelReference;
            //        pExistInContext.Cycle = product.Cycle;
            //        pExistInContext.LastUpdate = product.LastUpdate;

            //        _context.Update(pExistInContext);
            //        await _context.SaveChangesAsync();
            //        Console.WriteLine("product: " + pExistInContext.Id.ToString() + " - Atualizado com suceso");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }
            //}

            var pExistInContext = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (pExistInContext == null)
            {
                //Fazer Create
                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Product: " + product.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Name = product.Name;
                    pExistInContext.LabelReference = product.LabelReference;
                    pExistInContext.Cycle = product.Cycle;
                    pExistInContext.LastUpdate = product.LastUpdate;

                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Product: " + pExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        public static async Task UpdateDevice(Device device, ContextAcquisitonDb _context)
        {
            //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
            //neste caso o worker
            var linecontext = _context.Lines.SingleOrDefault(l => l.Id == device.LineId);
            if (linecontext == null)
            {
                //vai criar o worker e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateLine(device.Line, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateDevice(device, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //---------------------
            //agora só tem de fazer o update do coordinator pois apartir daqui o worker já existe
            var dExistInContext = _context.Devices.SingleOrDefault(d => d.Id == device.Id);
            if (dExistInContext == null)
            {
                //Create
                try
                {
                    device.Line = linecontext;
                    device.LineId = linecontext.Id;

                    _context.Add(device);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("device: " + device.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    dExistInContext.Line = linecontext;
                    dExistInContext.LineId = linecontext.Id;
                    //outras
                    dExistInContext.LastUpdate = device.LastUpdate;
                    dExistInContext.Type = device.Type;

                    _context.Update(dExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("device: " + dExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public static async Task UpdateStop(Stop stop, ContextAcquisitonDb _context)
        {
            //vai ter de ver se existem as lines
            var linecontext = _context.Lines.SingleOrDefault(l => l.Id == stop.LineId);
            if (linecontext == null)
            {
                //vai criar a line e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateLine(stop.Line, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateStop(stop, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //vai ter de ver tbm se existe a reason e esta pode ser null
            var reasoncontext = _context.Reasons.SingleOrDefault(r => r.Id == stop.ReasonId);
            if (stop.Reason != null)
            {
                if (reasoncontext == null)
                {
                    //vai criar a line e depois mandar fazer esta função de novo
                    try
                    {
                        //vai criar o worker
                        await UpdateReason(stop.Reason, _context);
                        //depois de dar o update do worker retira da lista pois já foi atualizado
                        await UpdateStop(stop, _context);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            //---------------------
            //agora só tem de fazer o update do stop 
            //criar
            var sExistInContext = _context.Stops.SingleOrDefault(s => s.Id == stop.Id);
            if (sExistInContext == null)
            {
                try
                {
                    if (stop.Reason != null)
                    {
                        stop.Reason = reasoncontext;
                        stop.ReasonId = reasoncontext.Id;
                    }
                    else
                    {
                        stop.Reason = null;
                        stop.ReasonId = null;
                    }
                    stop.Line = linecontext;
                    stop.LineId = linecontext.Id;

                    _context.Add(stop);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + stop.Id.ToString() + " - Adicionado com suceso");

                    //verificar se é para enviar aviso
                    await CheckIfIsUrgentStop(stop , _context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    if (stop.Reason != null)
                    {
                        sExistInContext.Reason = reasoncontext;
                        sExistInContext.ReasonId = reasoncontext.Id;
                    }
                    sExistInContext.Line = linecontext;
                    sExistInContext.LineId = linecontext.Id;
                    //o resto do stop
                    sExistInContext.Planned = stop.Planned;
                    sExistInContext.InitialDate = stop.InitialDate;
                    sExistInContext.EndDate = stop.EndDate;
                    sExistInContext.Duration = stop.Duration;
                    sExistInContext.Shift = stop.Shift;
                    sExistInContext.LastUpdate = stop.LastUpdate;

                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("stop: " + sExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public static async Task UpdateWorker(Worker worker, ContextAcquisitonDb _context)
        {
            var wExistInContext = _context.Workers.SingleOrDefault(w => w.Id == worker.Id);
            if (wExistInContext == null)
            {
                //Fazer Create
                try
                {
                    _context.Add(worker);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Worker: " + worker.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {

                    wExistInContext.IdFirebase = worker.IdFirebase;
                    wExistInContext.UserName = worker.UserName;
                    wExistInContext.Email = worker.Email;
                    wExistInContext.Role = worker.Role;
                    wExistInContext.LastUpdate = worker.LastUpdate;

                    _context.Update(wExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Worker: " + wExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }


        }

        public static async Task UpdateReason(Reason reason, ContextAcquisitonDb _context)
        {
            var rExistInContext = _context.Reasons.SingleOrDefault(r => r.Id == reason.Id);
            if (rExistInContext == null)
            {
                //Fazer Create
                try
                {
                    _context.Add(reason);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Reason: " + reason.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    rExistInContext.Description = reason.Description;
                    rExistInContext.LastUpdate = reason.LastUpdate;

                    _context.Update(rExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Reason: " + rExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }


        }

        public static async Task UpdateProductionPlan(Production_Plan production_Plan, ContextAcquisitonDb _context)
        {
            //vai ter de ver se existem as lines
            var linecontext = _context.Lines.SingleOrDefault(l => l.Id == production_Plan.LineId);
            if (linecontext == null)
            {
                //vai criar a line e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateLine(production_Plan.Line, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateProductionPlan(production_Plan, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //vai ter de ver tbm se existe a reason e esta pode ser null
            var productcontext = _context.Products.SingleOrDefault(p => p.Id == production_Plan.ProductId);
            if (productcontext == null)
            {
                //vai criar a line e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateProduct(production_Plan.Product, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateProductionPlan(production_Plan, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //---------------------
            //agora só tem de fazer o update do stop 
            //create
            var pExistInContext = _context.Production_Plans.SingleOrDefault(p => p.Id == production_Plan.Id);
            if (pExistInContext == null)
            {
                try
                {
                    production_Plan.Product = productcontext;
                    production_Plan.ProductId = productcontext.Id;
                    production_Plan.Line = linecontext;
                    production_Plan.LineId = linecontext.Id;

                    _context.Add(production_Plan);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("ProductionPlan: " + production_Plan.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Product = productcontext;
                    pExistInContext.ProductId = productcontext.Id;
                    pExistInContext.Line = linecontext;
                    pExistInContext.LineId = linecontext.Id;
                    //o resto do stop
                    pExistInContext.Goal = production_Plan.Goal;
                    pExistInContext.Name = production_Plan.Name;
                    pExistInContext.InitialDate = production_Plan.InitialDate;
                    pExistInContext.EndDate = production_Plan.EndDate;
                    pExistInContext.Shift = production_Plan.Shift;
                    pExistInContext.LastUpdate = production_Plan.LastUpdate;

                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("ProductionPlan: " + pExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task UpdateProduction(Production production, ContextAcquisitonDb _context)
        {
            //Em primeiro lugar vai ver se as outras classes que estão dentro do coordinator já existem
            //neste caso o worker
            var proPlancontext = _context.Production_Plans.SingleOrDefault(p => p.Id == production.Production_PlanId);
            if (proPlancontext == null)
            {
                //vai criar o worker e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateProductionPlan(production.Prod_Plan, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateProduction(production, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //---------------------
            //agora só tem de fazer o update da Production pois apartir daqui o 
            var pExistInContext = _context.Productions.SingleOrDefault(p => p.Id == production.Id);
            if (pExistInContext == null)
            {
                try
                {
                    production.Prod_Plan = proPlancontext;
                    production.Production_PlanId = proPlancontext.Id;

                    _context.Add(production);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Production: " + production.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    pExistInContext.Prod_Plan = proPlancontext;
                    pExistInContext.Production_PlanId = proPlancontext.Id;
                    //resto
                    pExistInContext.Hour = production.Hour;
                    pExistInContext.Day = production.Day;
                    pExistInContext.Quantity = production.Quantity;
                    pExistInContext.LastUpdate = production.LastUpdate;

                    _context.Update(pExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Production: " + pExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task UpdateSchedules(Schedule_Worker_Line schedule, ContextAcquisitonDb _context)
        {
            //vai ter de ver se existem as lines
            var linecontext = _context.Lines.SingleOrDefault(l => l.Id == schedule.LineId);
            if (linecontext == null)
            {
                //vai criar a line e depois mandar fazer esta função de novo
                try
                {
                    //vai criar o worker
                    await UpdateLine(schedule.Line, _context);
                    //depois de dar o update do worker retira da lista pois já foi atualizado
                    await UpdateSchedules(schedule, _context);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            //vai ter de ver tbm se existe o operator que pode ser null
            var operatorcontext = _context.Operators.SingleOrDefault(o => o.Id == schedule.OperatorId);
            if (schedule.Operator != null)
            {
                if (operatorcontext == null)
                {
                    //vai criar o operator e depois mandar fazer esta função de novo
                    try
                    {
                        //vai criar o worker
                        await UpdateOperator(schedule.Operator, _context);
                        //depois de dar o update do worker retira da lista pois já foi atualizado
                        await UpdateSchedules(schedule, _context);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            //vai ter de ver tbm se existe o operator que pode ser null
            var supervisorcontext = _context.Supervisors.SingleOrDefault(o => o.Id == schedule.SupervisorId);
            if (schedule.Supervisor != null)
            {
                if (supervisorcontext == null)
                {
                    //vai criar o operator e depois mandar fazer esta função de novo
                    try
                    {
                        //vai criar o worker
                        await UpdateSupervisor(schedule.Supervisor, _context);
                        //depois de dar o update do worker retira da lista pois já foi atualizado
                        await UpdateSchedules(schedule, _context);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            //---------------------
            //agora só tem de fazer o update do stop 
            var sExistInContext = _context.Schedule_Worker_Lines.SingleOrDefault(s => s.Id == schedule.Id);
            if (sExistInContext == null)
            {
                try
                {
                    //Create
                    if (schedule.Operator != null)
                    {
                        schedule.Operator = operatorcontext;
                        schedule.OperatorId = operatorcontext.Id;
                    }
                    if (schedule.Supervisor != null)
                    {
                        schedule.Supervisor = supervisorcontext;
                        schedule.SupervisorId = supervisorcontext.Id;
                    }

                    schedule.Line = linecontext;
                    schedule.LineId = linecontext.Id;

                    _context.Add(schedule);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Schedule: " + schedule.Id.ToString() + " - Adicionado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                //fazer update
                try
                {
                    if (schedule.Operator != null)
                    {
                        sExistInContext.Operator = operatorcontext;
                        sExistInContext.OperatorId = operatorcontext.Id;
                    }
                    else
                    {
                        sExistInContext.Operator = null;
                        sExistInContext.OperatorId = null;
                    }
                    if (schedule.Supervisor != null)
                    {
                        sExistInContext.Supervisor = supervisorcontext;
                        sExistInContext.SupervisorId = supervisorcontext.Id;
                    }
                    else
                    {
                        sExistInContext.Supervisor = null;
                        sExistInContext.SupervisorId = null;
                    }

                    sExistInContext.Line = linecontext;
                    sExistInContext.LineId = linecontext.Id;
                    //outros
                    sExistInContext.Day = schedule.Day;
                    sExistInContext.Shift = schedule.Shift;
                    sExistInContext.LastUpdate = schedule.LastUpdate;

                    _context.Update(sExistInContext);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Schedule: " + sExistInContext.Id.ToString() + " - Atualizado com suceso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        public static async Task UpdateComponentProducts(ComponentProduct compProduct, ContextAcquisitonDb _context)
        {

            //var cpExistInContext = _context.ComponentProducts.SingleOrDefault(c => c.Id == compProduct.Id);
            //if (cpExistInContext == null)
            //{
            //    try
            //    {
            //        compProduct.Product = null;
            //        compProduct.Component = null;
            //        _context.Add(compProduct);
            //        await _context.SaveChangesAsync();
            //        Console.WriteLine("ComponentProduct: " + compProduct.Id.ToString() + " - Adicionado com suceso");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }
            //}
            //else
            //{
            //    //fazer update
            //    try
            //    {
            //        cpExistInContext.ProductId = compProduct.ProductId;
            //        cpExistInContext.ComponentId = compProduct.ComponentId;

            //        _context.Update(cpExistInContext);
            //        await _context.SaveChangesAsync();
            //        Console.WriteLine("ComponentProduct: " + compProduct.Id.ToString() + " - Atualizado com suceso");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }
            //}


            _context.Add(compProduct);
            await _context.SaveChangesAsync();
        }

        public static async Task CheckIfIsUrgentStop(Stop stop, ContextAcquisitonDb _context)
        {
            try
            {
                TimeSpan ts = stop.EndDate.Subtract(stop.InitialDate);
                if(ts.TotalMinutes >= UrgentStopTime)
                {
                    //soar o aviso
                    AlertRequest ar = new AlertRequest();
                    ar.type = 0;//alerta de paragens
                    ar.line = stop.LineId;
                    ar.shift = stop.Shift;
                    ar.dateStart = stop.InitialDate;
                    ar.dateEnd = stop.EndDate;
                    if(stop.ReasonId != null)
                    {
                        var reason = _context.Reasons.First(r => r.Id == stop.ReasonId);
                        if(reason != null)
                        {
                            ar.message = reason.Description;
                        }                     
                    }

                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.PostAsJsonAsync(AlertAppConnectionString, ar);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Alerta da paragem - "+stop.Id.ToString()+ " Enviado com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Alerta da paragem - " + stop.Id.ToString() + " Erro ao enviar Alerta");
            }
        }

        


    }
}
