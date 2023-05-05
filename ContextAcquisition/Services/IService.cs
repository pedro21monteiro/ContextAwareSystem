using ContextAcquisition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAcquisition.Services
{
    public interface IService
    {
        Task<List<Component>> GetComponents(DateTime? DataInicial);
        Task<List<Coordinator>> GetCoordinators(DateTime? DataInicial);
        Task<List<Device>> GetDevices(DateTime? DataInicial);
        Task<List<Line>> GetLines(DateTime? DataInicial);
        Task<List<Operator>> GetOperators(DateTime? DataInicial);
        Task<List<Product>> GetProducts(DateTime? DataInicial);
        Task<List<Production>> GetProductions(DateTime? DataInicial);
        Task<List<Production_Plan>> GetProductionPlans(DateTime? DataInicial);
        Task<List<Reason>> GetReasons(DateTime? DataInicial);
        Task<List<Request>> GetRequests(DateTime? DataInicial);
        Task<List<Schedule_Worker_Line>> GetSchedule_Worker_Lines(DateTime? DataInicial);
        Task<List<Stop>> GetStops(DateTime? DataInicial);
        Task<List<Supervisor>> GetSupervisors(DateTime? DataInicial);
        Task<List<Worker>> GetWorkers(DateTime? DataInicial);




    }
}
