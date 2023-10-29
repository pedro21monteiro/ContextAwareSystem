using System;

public class DataService: IDataService
{
    private static string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
    private readonly HttpClient httpClient = new HttpClient();

    //Lista de serviços que podem ser pedidos à camada de integração com as bases de dados

    //--------------------------Serviços relacionados com Components-------------------------------------
    public async Task<List<Component>?> GetComponents(int? id, string? name, string? reference, int? category)
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
            return await httpClient.GetFromJsonAsync<List<Component>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponents/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Coordinator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetCoordinators/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Device>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetDevices/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetLines/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Operator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetOperators/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Product>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProducts/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Production>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProductions/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Production_Plan>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetProdPlans/" + searchLink); ;
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
            return await httpClient.GetFromJsonAsync<List<Reason>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetReasons/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSchedules/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Stop>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetStops/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Supervisor>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSupervisors/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<Worker>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetWorkers/" + searchLink);
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
            return await httpClient.GetFromJsonAsync<List<ComponentProduct>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetComponentProducts/" + searchLink); ;
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
}
