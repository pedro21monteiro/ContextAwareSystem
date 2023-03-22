using Models.Models;

namespace ContextServer.Services

{
    public interface IService
    {
        //Devices
        Task<List<Device>> GetDevices();

        //Workers
        Task<List<Worker>> GetWorkers();


        //Lines
        Task<List<Line>> GetLines();
        //Stops
        Task<List<Stop>> GetStops();
        //Productions
        Task<List<Production>> GetProductions();
        //Products
        Task<List<Product>> GetProducts();
        //Products
        Task<List<Component>> GetComponents();
 
        //operators
        Task<List<Operator>> GetOperators();
        //coordinators
        Task<List<Coordinator>> GetCoordinators();
        //Reasons
        Task<List<Reason>> GetReasons();
        //Schedule_Worker_Lines
        Task<List<Schedule_Worker_Line>> GetSchedule_Worker_Lines();
        //Supervisors
        Task<List<Supervisor>> GetSupervisors();
    }
}
