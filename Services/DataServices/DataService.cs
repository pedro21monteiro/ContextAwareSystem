using Models.cdc_Models;
using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices
{
    public class DataService : IDataService
    {
        private static readonly string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
        private static readonly string IntegrationLayerConnectionString = $"{continentalTestAPIHost}/api/ContinentalAPI";


        private readonly HttpClient httpClient = new HttpClient();

        //Lista de serviços que podem ser pedidos à camada de integração com as bases de dados

        //--------------------------Serviços relacionados com Components-------------------------------------
        public async Task<List<Component>?> GetComponents(int? id, string? name, string? reference, int? category)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (!string.IsNullOrEmpty(name))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}name={name}";
            }
            if (!string.IsNullOrEmpty(reference))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}reference={reference}";
            }
            if (category != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}category={category}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<Component>>($"{IntegrationLayerConnectionString}/GetComponents/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Component?> GetComponentById(int id)
        {
            try
            {
                var listComponents = await GetComponents(id, null, null, null);
                return listComponents?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Coordinators-------------------------------------
        public async Task<List<Coordinator>?> GetCoordinators(int? id, int? workerId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (workerId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}workerId={workerId}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Coordinator>>($"{IntegrationLayerConnectionString}/GetCoordinators/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Coordinator?> GetCoordinatorByWorkerId(int workerId)
        {
            try
            {
                var listCoordinators = await GetCoordinators(null, workerId);
                return listCoordinators?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Coordinator?> GetCoordinatorById(int id)
        {
            try
            {
                var listCoordinators = await GetCoordinators(id, null);
                return listCoordinators?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Devices-------------------------------------
        
        public async Task<List<Device>?> GetDevices(int? id, int? type, int? lineId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (type != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}type={type}";
            }
            if (lineId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}lineId={lineId}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Device>>($"{IntegrationLayerConnectionString}/GetDevices/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        public async Task<Device?> GetDeviceById(int id)
        {
            try
            {
                var listDevices = await GetDevices(id, null, null);
                return listDevices?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Lines-------------------------------------
        public async Task<List<Line>?> GetLines(int? id, string? name, bool? priority, int? coordinatorId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (!string.IsNullOrEmpty(name))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}name={name}";
            }
            if (priority != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}priority={priority}";
            }
            if (coordinatorId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}coordinatorId={coordinatorId}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<Line>>($"{IntegrationLayerConnectionString}/GetLines/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Line?> GetLineById(int id)
        {
            try
            {
                var listLines = await GetLines(id, null, null, null);
                return listLines?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<List<Line>?> GetLinesByCoordinatorId(int coordinatorId)
        {
            try
            {
                return await GetLines(null, null, null, coordinatorId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Operators-------------------------------------
        public async Task<List<Operator>?> GetOperators(int? id, int? workerId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (workerId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}workerId={workerId}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<Operator>>($"{IntegrationLayerConnectionString}/GetOperators/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Operator?> GetOperatorByWorkerId(int workerId)
        {
            try
            {
                var listOperators = await GetOperators(null, workerId);
                return listOperators?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Products-------------------------------------
        public async Task<List<Product>?> GetProducts(int? id, string? name, string? labelReference, TimeSpan? cycle)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (!string.IsNullOrEmpty(name))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}name={name}";
            }
            if (!string.IsNullOrEmpty(labelReference))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}labelReference={labelReference}";
            }
            if (cycle != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}cycle={cycle}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Product>>($"{IntegrationLayerConnectionString}/GetProducts/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                var listProducts = await GetProducts(id, null, null, null);
                return listProducts?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Productions-------------------------------------
        public async Task<List<Production>?> GetProductions(int? id, int? hour, DateTime? day, int? quantity, int? prodPlanId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (hour != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}hour={hour}";
            }
            if (day != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}day={day.Value.ToString("yyyy-MM-dd")}";
            }
            if (quantity != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}quantity={quantity}";
            }
            if (prodPlanId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}prodPlanId={prodPlanId}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<Production>>($"{IntegrationLayerConnectionString}/GetProductions/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        public async Task<List<Production>?> GetProductionsByProdPlanId(int prodPlanid)
        {
            try
            {
                return await GetProductions(null, null, null, null, prodPlanid);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com productionPlans-------------------------------------
        public async Task<List<Production_Plan>?> GetProdPlans(int? id, int? goal, string? name, DateTime? inicialDate, DateTime? endDate, int? productId, int? lineId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (goal != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}goal={goal}";
            }
            if (!string.IsNullOrEmpty(name))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}name={name}";
            }
            if (inicialDate != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}inicialDate={inicialDate.Value.ToString("yyyy-MM-dd")}";
            }
            if (endDate != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}endDate={endDate.Value.ToString("yyyy-MM-dd")}";
            }
            if (productId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}productId={productId}";
            }
            if (lineId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}lineId={lineId}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Production_Plan>>($"{IntegrationLayerConnectionString}/GetProdPlans/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        public async Task<List<Production_Plan>?> GetProdPlansByLineId(int lineId)
        {
            try
            {
                return await GetProdPlans(null, null, null, null, null, null, lineId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        public async Task<Production_Plan?> GetProdPlanById(int id)
        {
            try
            {
                var listProductionPlans = await GetProdPlans(id, null, null, null, null, null, null);
                return listProductionPlans?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        //--------------------------Serviços relacionados com Reasons-------------------------------------
        public async Task<List<Reason>?> GetReasons(int? id, string? description)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (!string.IsNullOrEmpty(description))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}description={description}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Reason>>($"{IntegrationLayerConnectionString}/GetReasons/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Reason?> GetReasonById(int id)
        {
            try
            {
                var listReasons = await GetReasons(id, null);
                return listReasons?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Schedules-------------------------------------
        public async Task<List<Schedule_Worker_Line>?> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (day != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}day={day.Value.ToString("yyyy-MM-dd")}";
            }
            if (shift != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}shift={shift}";
            }
            if (lineId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}lineId={lineId}";
            }
            if (operatorId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}operatorId={operatorId}";
            }
            if (supervisorId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}supervisorId={supervisorId}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"{IntegrationLayerConnectionString}/GetSchedules/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<List<Schedule_Worker_Line>?> GetSchedulesByOperatorId(int operatorId)
        {
            try
            {
                return await GetSchedules(null, null, null, null, operatorId, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<List<Schedule_Worker_Line>?> GetSchedulesBySupervisorId(int SupervisorId)
        {
            try
            {
                return await GetSchedules(null, null, null, null, null, SupervisorId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Stops-------------------------------------
        public async Task<List<Stop>?> GetStops(int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (planned != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}planned={planned}";
            }
            if (initialDate != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}initialDate={initialDate.Value.ToString("yyyy-MM-dd")}";
            }
            if (endDate != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}endDate={endDate.Value.ToString("yyyy-MM-dd")}";
            }
            if (duration != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}duration={duration}";
            }
            if (shift != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}shift={shift}";
            }
            if (reasonId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}reasonId={reasonId}";
            }
            if (lineId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}lineId={lineId}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<Stop>>($"{IntegrationLayerConnectionString}/GetStops/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<List<Stop>?> GetStopsByLineId(int lineId)
        {
            try
            {
                return await GetStops(null, null, null, null, null, null, lineId, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        //--------------------------Serviços relacionados com Supervisors-------------------------------------
        public async Task<List<Supervisor>?> GetSupervisors(int? id, int? workerId)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (workerId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}workerId={workerId}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<Supervisor>>($"{IntegrationLayerConnectionString}/GetSupervisors/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Supervisor?> GetSupervisorByWorkerId(int workerId)
        {
            try
            {
                var listSupervisotrs = await GetSupervisors(null, workerId);
                return listSupervisotrs?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com Workers-------------------------------------
        public async Task<List<Worker>?> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (!string.IsNullOrEmpty(idFirebase))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}idFirebase={idFirebase}";
            }
            if (!string.IsNullOrEmpty(username))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}username={username}";
            }
            if (!string.IsNullOrEmpty(email))
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}email={email}";
            }
            if (role != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}role={role}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<Worker>>($"{IntegrationLayerConnectionString}/GetWorkers/{searchLink}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Worker?> GetWorkerByIdFirebase(string idFirebase)
        {
            try
            {
                var listWorkers = await GetWorkers(null, idFirebase, null, null, null);
                return listWorkers?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<Worker?> GetWorkerById(int id)
        {
            try
            {
                var listWorkers = await GetWorkers(id, null, null, null, null);
                return listWorkers?.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com ComponentProducts-------------------------------------
        public async Task<List<ComponentProduct>?> GetComponentProducts(int? id, int? componentId, int? productId, int? quantidade)
        {
            string searchLink = "?";
            if (id != null)
            {
                searchLink += $"id={id}";
            }
            if (componentId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}componentId={componentId}";
            }
            if (productId != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}productId={productId}";
            }
            if (quantidade != null)
            {
                searchLink += $"{(searchLink != "?" ? "&" : "")}quantidade={quantidade}";
            }

            try
            {
                return await httpClient.GetFromJsonAsync<List<ComponentProduct>>($"{IntegrationLayerConnectionString}/GetComponentProducts/{searchLink}"); ;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
        public async Task<List<ComponentProduct>?> GetComponentProductsByProductId(int productId)
        {
            try
            {
                return await GetComponentProducts(null, null, productId, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com cdc_Stops-------------------------------------
        public async Task<List<CDC_Stop>?> GetCDC_Stops(DateTime? LastVerification)
        {
            string searchLink = string.Empty;
            if (LastVerification != null)
            {
                searchLink = $"?InicialDate={LastVerification.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<CDC_Stop>>($"{IntegrationLayerConnectionString}/GetCdcStops/{searchLink}"); ;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }

        //--------------------------Serviços relacionados com cdc_Productions-------------------------------------
        public async Task<List<CDC_Production>?> GetCDC_Productions(DateTime? LastVerification)
        {
            string searchLink = string.Empty;
            if (LastVerification != null)
            {
                searchLink = $"?InicialDate={LastVerification.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")}";
            }
            try
            {
                return await httpClient.GetFromJsonAsync<List<CDC_Production>>($"{IntegrationLayerConnectionString}/GetCdcProductions/{searchLink}"); ;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocorreu uma exceção: {e.Message}");
                return null;
            }
        }
    }
}
