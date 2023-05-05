using ContextAcquisition.Models;
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

        public Service(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
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
                    return await httpClient.GetFromJsonAsync<List<Component>>("https://localhost:7013/api/ContinentalAPI/GetComponents");
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
                    return await httpClient.GetFromJsonAsync<List<Component>>($"https://localhost:7013/api/ContinentalAPI/GetComponents/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Coordinator>>("https://localhost:7013/api/ContinentalAPI/GetCoordinators");
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
                    return await httpClient.GetFromJsonAsync<List<Coordinator>>($"https://localhost:7013/api/ContinentalAPI/GetCoordinators/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Device>>("https://localhost:7013/api/ContinentalAPI/GetDevices");
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
                    return await httpClient.GetFromJsonAsync<List<Device>>($"https://localhost:7013/api/ContinentalAPI/GetDevices/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Line>>("https://localhost:7013/api/ContinentalAPI/GetLines");
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
                    return await httpClient.GetFromJsonAsync<List<Line>>($"https://localhost:7013/api/ContinentalAPI/GetLines/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Operator>>("https://localhost:7013/api/ContinentalAPI/GetOperators");
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
                    return await httpClient.GetFromJsonAsync<List<Operator>>($"https://localhost:7013/api/ContinentalAPI/GetOperators/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7013/api/ContinentalAPI/GetProducts");
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
                    return await httpClient.GetFromJsonAsync<List<Product>>($"https://localhost:7013/api/ContinentalAPI/GetProducts/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Production>>("https://localhost:7013/api/ContinentalAPI/GetProductions");
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
                    return await httpClient.GetFromJsonAsync<List<Production>>($"https://localhost:7013/api/ContinentalAPI/GetProductions/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Production_Plan>>("https://localhost:7013/api/ContinentalAPI/GetProductionPlans");
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
                    return await httpClient.GetFromJsonAsync<List<Production_Plan>>($"https://localhost:7013/api/ContinentalAPI/GetProductionPlans/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Reason>>("https://localhost:7013/api/ContinentalAPI/GetReasons");
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
                    return await httpClient.GetFromJsonAsync<List<Reason>>($"https://localhost:7013/api/ContinentalAPI/GetReasons/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Request>>("https://localhost:7013/api/ContinentalAPI/GetRequests");
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
                    return await httpClient.GetFromJsonAsync<List<Request>>($"https://localhost:7013/api/ContinentalAPI/GetRequest/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>("https://localhost:7013/api/ContinentalAPI/GetSchedule_Worker_Lines");
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
                    return await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"https://localhost:7013/api/ContinentalAPI/GetSchedule_Worker_Lines/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Stop>>("https://localhost:7013/api/ContinentalAPI/GetStops");
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
                    return await httpClient.GetFromJsonAsync<List<Stop>>($"https://localhost:7013/api/ContinentalAPI/GetStops/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Supervisor>>("https://localhost:7013/api/ContinentalAPI/GetSupervisors");
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
                    return await httpClient.GetFromJsonAsync<List<Supervisor>>($"https://localhost:7013/api/ContinentalAPI/GetSupervisors/" + "?InicialDate=" + DataInicial.ToString());
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
                    return await httpClient.GetFromJsonAsync<List<Worker>>("https://localhost:7013/api/ContinentalAPI/GetWorkers");
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
                    return await httpClient.GetFromJsonAsync<List<Worker>>($"https://localhost:7013/api/ContinentalAPI/GetWorkers/" + "?InicialDate=" + DataInicial.ToString());
                }
                catch (Exception e)
                {
                    return null;
                    Console.WriteLine(e.ToString());
                }

            }

        }
    }
}
