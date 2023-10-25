

using Models.ContextModels;

namespace ContextServer.Services

{
    public interface IDataService
    {
        //-------------------------Components
        Task<List<Component>> GetComponents(int? id, string? name, string? reference, int? category);

        //-------------------------Coordinators
        Task<List<Coordinator>> GetCoordinators(int? id, int? workerId);

        //-------------------------Devices
        Task<List<Device>> GetDevices(int? id, int? type, int? lineId);
        Task<Device> GetDeviceById(int id);

        //-------------------------Lines
        Task<List<Line>> GetLines(int? id, string? name, bool? priority, int? coordinatorId);
        Task<Line> GetLineById(int id);

        //-------------------------Operators
        Task<List<Operator>> GetOperators(int? id, int? workerId);
        Task<Operator> GetOperatorByWorkerId(int sorkerId);

        //-------------------------Productions
        Task<List<Production>> GetProductions(int? id, int? hour, DateTime? day, int? quantity, int? prodPlanId);

        //-------------------------ProductionsPlans
        Task<List<Production_Plan>> GetProdPlans(int? id, int? goal, string? name, DateTime? inicialDate, DateTime? endDate, int? productId, int? lineId);

        //-------------------------Reasons
        Task<List<Reason>> GetReasons(int? id, string? description);

        //-------------------------Schedules
        Task<List<Schedule_Worker_Line>> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId);
        Task<List<Schedule_Worker_Line>> GetSchedulesByOperatorId(int operatorId);

        //-------------------------Stops
        Task<List<Stop>> GetStops(int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId);

        //-------------------------Supervisors
        Task<List<Supervisor>> GetSupervisors(int? id, int? workerId);

        //-------------------------Workers
        Task<List<Worker>> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role);
        Task<Worker> GetWorkerByIdFirebase(string idFirebase);

        //-------------------------ComponentProducts
        Task<List<ComponentProduct>> GetComponentProducts(int? id, int? componentId, int? productId, int? quantidade);

    }
}
