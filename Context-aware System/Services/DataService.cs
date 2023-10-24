

using Models.ContextModels;

namespace ContextServer.Services

{
    public class DataService : IDataService
    {
        private static string continentalTestAPIHost = System.Environment.GetEnvironmentVariable("CONTAPI") ?? "https://localhost:7013";
        private readonly HttpClient httpClient = new HttpClient();



        //Lista de serviços que podem ser pedidos à camada de integração com as bases de dados

        //--------------------------Serviços relacionados com Devices-------------------------------------

        public async Task<Device> GetDeviceById(int id)
        {
            try
            {
                List<Device> listDevices = new List<Device>();
                listDevices = await httpClient.GetFromJsonAsync<List<Device>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetDevices/" + "?Id=" + id.ToString());
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

        //--------------------------Serviços relacionados com Workers-------------------------------------
        public async Task<List<Worker>> GetWorkersByIdFirebase(string idFirebase)
        {
            try
            {
                List<Worker> listWorkers = new List<Worker>();
                listWorkers = await httpClient.GetFromJsonAsync<List<Worker>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetWorkers/" + "?idFirebase=" + idFirebase);
                return listWorkers;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Lines-------------------------------------
        public async Task<List<Line>> GetLinesById(int id)
        {
            try
            {
                List<Line> listLines = new List<Line>();
                listLines = await httpClient.GetFromJsonAsync<List<Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetLines/" + "?Id=" + id.ToString());
                return listLines;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com ProductionLines-------------------------------------

        //--------------------------Serviços relacionados com Stops-------------------------------------

        //--------------------------Serviços relacionados com Productions-------------------------------------

        //--------------------------Serviços relacionados com Products-------------------------------------

        //--------------------------Serviços relacionados com Components-------------------------------------

        //--------------------------Serviços relacionados com Product_Componets-------------------------------------


        //--------------------------Serviços relacionados com Operators-------------------------------------
        public async Task<List<Operator>> GetOperatorsByWorkerId(int WorkerId)
        {
            try
            {
                List<Operator> listOperators = new List<Operator>();
                listOperators = await httpClient.GetFromJsonAsync<List<Operator>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetOperators/" + "?workerId=" + WorkerId.ToString());
                return listOperators;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //--------------------------Serviços relacionados com Schedules-------------------------------------
        public async Task<List<Schedule_Worker_Line>> GetSchedulesByOperatorId(int OperatorId)
        {
            try
            {
                List<Schedule_Worker_Line> listSchedules = new List<Schedule_Worker_Line>();
                listSchedules = await httpClient.GetFromJsonAsync<List<Schedule_Worker_Line>>($"{continentalTestAPIHost}/api/ContinentalAPI/GetSchedules/" + "?operatorId=" + OperatorId.ToString());
                return listSchedules;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        //--------------------------Serviços relacionados com Coordinators-------------------------------------


        //--------------------------Serviços relacionados com GetReasons-------------------------------------


        //--------------------------Serviços relacionados com GetReasons-------------------------------------

        //--------------------------Serviços relacionados com Operators-------------------------------------

    }
}
