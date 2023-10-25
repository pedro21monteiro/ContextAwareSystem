

using Models.ContextModels;

namespace ContextServer.Services

{
    public class DataService : IDataService
    {
        private static string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
        private readonly HttpClient httpClient = new HttpClient();

        //Lista de serviços que podem ser pedidos à camada de integração com as bases de dados

        //--------------------------Serviços relacionados com Components-------------------------------------
        public async Task<List<Component>> GetComponents(int? id, string? name, string? reference, int? category)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "name=" + name;
            }
            if (!string.IsNullOrEmpty(reference))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "reference=" + reference;
            }
            if (category != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "category=" + category.ToString();
            }

            try
            {
                List<Component> listComponents = new List<Component>();
                listComponents = await httpClient.GetFromJsonAsync<List<Component>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponents/" + searchLink);
                return listComponents;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Coordinators-------------------------------------
        public async Task<List<Coordinator>> GetCoordinators(int? id, int? workerId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (workerId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "workerId=" + workerId.ToString();
            }

            try
            {
                List<Coordinator> listCoordinators = new List<Coordinator>();
                listCoordinators = await httpClient.GetFromJsonAsync<List<Coordinator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetCoordinators/" + searchLink);
                return listCoordinators;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Devices-------------------------------------
        public async Task<List<Device>> GetDevices(int? id, int? type, int? lineId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (type != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "type=" + type.ToString();
            }
            if (lineId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "lineId=" + lineId.ToString();
            }

            try
            {
                List<Device> listDevices = new List<Device>();
                listDevices = await httpClient.GetFromJsonAsync<List<Device>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetDevices/" + searchLink);
                return listDevices;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Device> GetDeviceById(int id)
        {
            try
            {
                var listDevices = await GetDevices(id,null,null);
                if (listDevices != null)
                {
                    var device = listDevices.FirstOrDefault();
                    if (device != null)
                    {
                        return device;
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Lines-------------------------------------
        public async Task<List<Line>> GetLines(int? id, string? name, bool? priority, int? coordinatorId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "name=" + name;
            }
            if (priority != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "priority=" + priority.ToString();
            }
            if (coordinatorId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "coordinatorId=" + coordinatorId.ToString();
            }

            try
            {
                List<Line> listLines = new List<Line>();
                listLines = await httpClient.GetFromJsonAsync<List<Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetLines/" + searchLink);
                return listLines;
            }
            catch (Exception e)
            {
                return null; // Lide adequadamente com exceções aqui
            }
        }
        public async Task<Line> GetLineById(int id)
        {
            try
            {
                var listLines = await GetLines(id,null,null,null);
                if (listLines != null)
                {
                    var line = listLines.FirstOrDefault();
                    if (line != null)
                    {
                        return line;
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Operators-------------------------------------
        public async Task<List<Operator>> GetOperators(int? id, int? workerId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (workerId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "workerId=" + workerId.ToString();
            }

            try
            {
                List<Operator> listOperators = new List<Operator>();
                listOperators = await httpClient.GetFromJsonAsync<List<Operator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetOperators/" + searchLink);
                return listOperators;
            }
            catch (Exception e)
            {
                return null; // Lide adequadamente com exceções aqui
            }
        }
        public async Task<Operator> GetOperatorByWorkerId(int workerId)
        {
            try
            {
                var listOperators = await GetOperators(null, workerId);
                if(listOperators != null)
                {
                    var ope = listOperators.FirstOrDefault();
                    if (ope != null)
                    {
                        return ope;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Products-------------------------------------
        public async Task<List<Product>> GetProducts(int? id, string? name, string? labelReference, TimeSpan? cycle)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "name=" + name;
            }
            if (!string.IsNullOrEmpty(labelReference))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "labelReference=" + labelReference;
            }
            if (cycle != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "cycle=" + cycle.ToString();
            }
            try
            {
                List<Product> listProducts = new List<Product>();
                listProducts = await httpClient.GetFromJsonAsync<List<Product>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProducts/" + searchLink);
                return listProducts;
            }
            catch (Exception e)
            {
                return null; 
            }
        }

        //--------------------------Serviços relacionados com Productions-------------------------------------
        public async Task<List<Production>> GetProductions(int? id, int? hour, DateTime? day, int? quantity, int? prodPlanId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (hour != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "hour=" + hour.ToString();
            }
            if (day != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "day=" + day.Value.ToString("yyyy-MM-dd");
            }
            if (quantity != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "quantity=" + quantity.ToString();
            }
            if (prodPlanId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "prodPlanId=" + prodPlanId.ToString();
            }

            try
            {
                List<Production> listProductions = new List<Production>();
                listProductions = await httpClient.GetFromJsonAsync<List<Production>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProductions/" + searchLink);
                return listProductions;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com productionPlans-------------------------------------
        public async Task<List<Production_Plan>> GetProdPlans(int? id, int? goal, string? name, DateTime? inicialDate, DateTime? endDate, int? productId, int? lineId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (goal != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "goal=" + goal.ToString();
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "name=" + name;
            }
            if (inicialDate != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "inicialDate=" + inicialDate.Value.ToString("yyyy-MM-dd");
            }
            if (endDate != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "endDate=" + endDate.Value.ToString("yyyy-MM-dd");
            }
            if (productId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "productId=" + productId.ToString();
            }
            if (lineId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "lineId=" + lineId.ToString();
            }
            try
            {
                List<Production_Plan> listProdPlans = new List<Production_Plan>();
                listProdPlans = await httpClient.GetFromJsonAsync<List<Production_Plan>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProdPlans/" + searchLink);
                return listProdPlans;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Reasons-------------------------------------
        public async Task<List<Reason>> GetReasons(int? id, string? description)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (!string.IsNullOrEmpty(description))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "description=" + description;
            }

            try
            {
                List<Reason> listReasons = new List<Reason>();
                listReasons = await httpClient.GetFromJsonAsync<List<Reason>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetReasons/" + searchLink);
                return listReasons;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Schedules-------------------------------------
        public async Task<List<Schedule_Worker_Line>> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (day != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "day=" + day.Value.ToString("yyyy-MM-dd");
            }
            if (shift != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "shift=" + shift.ToString();
            }
            if (lineId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "lineId=" + lineId.ToString();
            }
            if (operatorId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "operatorId=" + operatorId.ToString();
            }
            if (supervisorId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "supervisorId=" + supervisorId.ToString();
            }

            try
            {
                List<Schedule_Worker_Line> listSchedules = new List<Schedule_Worker_Line>();
                listSchedules = await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSchedules/" + searchLink);
                return listSchedules;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Schedule_Worker_Line>> GetSchedulesByOperatorId(int operatorId)
        {
            try
            {
                List<Schedule_Worker_Line> listSchedules = new List<Schedule_Worker_Line>();
                listSchedules = await GetSchedules(null, null, null, null, operatorId, null);
                return listSchedules;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Stops-------------------------------------
        public async Task<List<Stop>> GetStops(int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (planned != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "planned=" + planned.ToString();
            }
            if (initialDate != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "initialDate=" + initialDate.Value.ToString("yyyy-MM-dd");
            }
            if (endDate != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "endDate=" + endDate.Value.ToString("yyyy-MM-dd");
            }
            if (duration != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "duration=" + duration.ToString();
            }
            if (shift != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "shift=" + shift.ToString();
            }
            if (reasonId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "reasonId=" + reasonId.ToString();
            }
            if (lineId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "lineId=" + lineId.ToString();
            }

            try
            {
                List<Stop> listStops = new List<Stop>();
                listStops = await httpClient.GetFromJsonAsync<List<Stop>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetStops/" + searchLink);
                return listStops;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Supervisors-------------------------------------
        public async Task<List<Supervisor>> GetSupervisors(int? id, int? workerId)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (workerId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "workerId=" + workerId.ToString();
            }

            try
            {
                List<Supervisor> listSupervisors = new List<Supervisor>();
                listSupervisors = await httpClient.GetFromJsonAsync<List<Supervisor>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSupervisors/" + searchLink);
                return listSupervisors;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Workers-------------------------------------
        public async Task<List<Worker>> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (!string.IsNullOrEmpty(idFirebase))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "idFirebase=" + idFirebase;
            }
            if (!string.IsNullOrEmpty(username))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "username=" + username;
            }
            if (!string.IsNullOrEmpty(email))
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "email=" + email;
            }
            if (role != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "role=" + role.ToString();
            }

            try
            {
                List<Worker> listWorkers = new List<Worker>();
                listWorkers = await httpClient.GetFromJsonAsync<List<Worker>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetWorkers/" + searchLink);
                return listWorkers;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<Worker> GetWorkerByIdFirebase(string idFirebase)
        {
            try
            {              
                var listWorkers = await GetWorkers(null, idFirebase, null, null, null);
                if (listWorkers != null)
                {
                    var worker = listWorkers.FirstOrDefault();
                    if(worker != null)
                    {
                        return worker;
                    }
                    else
                    {
                        return null;
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com ComponentProducts-------------------------------------
        public async Task<List<ComponentProduct>> GetComponentProducts(int? id, int? componentId, int? productId, int? quantidade)
        {
            string searchLink = string.Empty;
            if (id != null)
            {
                searchLink = "?id=" + id.ToString();
            }
            if (componentId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "componentId=" + componentId.ToString();
            }
            if (productId != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "productId=" + productId.ToString();
            }
            if (quantidade != null)
            {
                if (!string.IsNullOrEmpty(searchLink))
                {
                    searchLink += "&";
                }
                else
                {
                    searchLink += "?";
                }
                searchLink += "quantidade=" + quantidade.ToString();
            }

            try
            {
                List<ComponentProduct> listComponentProducts = new List<ComponentProduct>();
                listComponentProducts = await httpClient.GetFromJsonAsync<List<ComponentProduct>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponentProducts/" + searchLink);
                return listComponentProducts;
            }
            catch (Exception e)
            {
                return null; 
            }
        }

    }
}
