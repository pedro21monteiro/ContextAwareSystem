using Models.Models;

namespace Context_aware_System.Services

{
    public class Service : IService
    {

        private readonly HttpClient httpClient;

        public Service(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }


        //Lista de serviços que podem ser pedidos à camada de integração com as bases de dados

        //--------------------------Serviços relacionados com Devices-------------------------------------

        public async Task<List<Device>> GetDevices()
        {
            return await httpClient.GetFromJsonAsync<List<Device>>("https://localhost:7057/api/Data/GetDevices");              
        }

        //--------------------------Serviços relacionados com Workers-------------------------------------
        public async Task<List<Worker>> GetWorkers()
        {
            return await httpClient.GetFromJsonAsync<List<Worker>>("https://localhost:7057/api/Data/GetWorkers");
        }

        //--------------------------Serviços relacionados com Machines-------------------------------------
        public async Task<List<Machine>> GetMachines()
        {
            return await httpClient.GetFromJsonAsync<List<Machine>>("https://localhost:7057/api/Data/GetMachines");
        }
        //--------------------------Serviços relacionados com ProductionLines-------------------------------------
        public async Task<List<Line>> GetLines()
        {
            return await httpClient.GetFromJsonAsync<List<Line>>("https://localhost:7057/api/Data/GetLines");
        }
        //--------------------------Serviços relacionados com Stops-------------------------------------
        public async Task<List<Stop>> GetStops()
        {
            return await httpClient.GetFromJsonAsync<List<Stop>>("https://localhost:7057/api/Data/GetStops");
        }
        //--------------------------Serviços relacionados com Productions-------------------------------------
        public async Task<List<Production>> GetProductions()
        {
            return await httpClient.GetFromJsonAsync<List<Production>>("https://localhost:7057/api/Data/GetProductions");
        }
        //--------------------------Serviços relacionados com Products-------------------------------------
        public async Task<List<Product>> GetProducts()
        {
            return await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7057/api/Data/GetProducts");
        }
        //--------------------------Serviços relacionados com Components-------------------------------------
        public async Task<List<Component>> GetComponents()
        {
            return await httpClient.GetFromJsonAsync<List<Component>>("https://localhost:7057/api/Data/GetComponents");
        }
        //--------------------------Serviços relacionados com Product_Componets-------------------------------------
        public async Task<List<Product_Component>> GetProduct_Components()
        {
            return await httpClient.GetFromJsonAsync<List<Product_Component>>("https://localhost:7057/api/Data/GetProduct_Components");
        }

        //--------------------------Serviços relacionados com Operators-------------------------------------
        public async Task<List<Operator>> GetOperators()
        {
            return await httpClient.GetFromJsonAsync<List<Operator>>("https://localhost:7057/api/Data/GetOperators");
        }

        //--------------------------Serviços relacionados com Coordinators-------------------------------------
        public async Task<List<Coordinator>> GetCoordinators()
        {
            return await httpClient.GetFromJsonAsync<List<Coordinator>>("https://localhost:7057/api/Data/GetCoordinators");
        }

        //--------------------------Serviços relacionados com GetReasons-------------------------------------
        public async Task<List<Reason>> GetReasons()
        {
            return await httpClient.GetFromJsonAsync<List<Reason>>("https://localhost:7057/api/Data/GetReasons");
        }

        //--------------------------Serviços relacionados com GetReasons-------------------------------------
        public async Task<List<Schedule_Worker_Line>> GetSchedule_Worker_Lines()
        {
            return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>("https://localhost:7057/api/Data/GetSchedule_Worker_Lines");
        }
        //--------------------------Serviços relacionados com Operators-------------------------------------
        public async Task<List<Supervisor>> GetSupervisors()
        {
            return await httpClient.GetFromJsonAsync<List<Supervisor>>("https://localhost:7057/api/Data/GetSupervisors");
        }
    }
}
