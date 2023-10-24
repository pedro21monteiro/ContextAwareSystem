

using Models.ContextModels;

namespace ContextServer.Services

{
    public interface IDataService
    {
        //Devices
        Task<Device> GetDeviceById(int id);

        //Workers
        Task<List<Worker>> GetWorkersByIdFirebase(string idFirebase);

        ////Lines
        //Task<List<Line>> GetLines();
        Task<List<Line>> GetLinesById(int id);
        ////Stops
        //Task<List<Stop>> GetStops();
        ////Productions
        //Task<List<Production>> GetProductions();
        ////Products
        //Task<List<Product>> GetProducts();
        ////Products
        //Task<List<Component>> GetComponents();

        //operators
        Task<List<Operator>> GetOperatorsByWorkerId(int WorkerId);
        //Task<List<Operator>> GetOperators();
        ////coordinators
        //Task<List<Coordinator>> GetCoordinators();
        ////Reasons
        //Task<List<Reason>> GetReasons();
        //Schedules
        Task<List<Schedule_Worker_Line>> GetSchedulesByOperatorId(int OperatorId);
        //Task<List<Schedule_Worker_Line>> GetSchedule_Worker_Lines();
        ////Supervisors
        //Task<List<Supervisor>> GetSupervisors();
    }
}
