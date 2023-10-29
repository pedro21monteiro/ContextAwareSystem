using System;

public interface IDataService
{
    //-------------------------Components
    Task<List<Component>?> GetComponents(int? id, string? name, string? reference, int? category);
    Task<Component?> GetComponentById(int id);

    //-------------------------Coordinators
    Task<List<Coordinator>?> GetCoordinators(int? id, int? workerId);
    Task<Coordinator?> GetCoordinatorByWorkerId(int workerId);
    Task<Coordinator?> GetCoordinatorById(int id);

    //-------------------------Devices
    Task<List<Device>?> GetDevices(int? id, int? type, int? lineId);
    Task<Device?> GetDeviceById(int id);

    //-------------------------Lines
    Task<List<Line>?> GetLines(int? id, string? name, bool? priority, int? coordinatorId);
    Task<Line?> GetLineById(int id);
    Task<List<Line>?> GetLinesByCoordinatorId(int coordinatorId);

    //-------------------------Operators
    Task<List<Operator>?> GetOperators(int? id, int? workerId);
    Task<Operator?> GetOperatorByWorkerId(int sorkerId);
    //-------------------------Products
    Task<List<Product>?> GetProducts(int? id, string? name, string? labelReference, TimeSpan? cycle);
    Task<Product?> GetProductById(int id);

    //-------------------------Productions
    Task<List<Production>?> GetProductions(int? id, int? hour, DateTime? day, int? quantity, int? prodPlanId);
    Task<List<Production>?> GetProductionsByProdPlanId(int prodPlanId);

    //-------------------------ProductionsPlans
    Task<List<Production_Plan>?> GetProdPlans(int? id, int? goal, string? name, DateTime? inicialDate, DateTime? endDate, int? productId, int? lineId);
    Task<List<Production_Plan>?> GetProdPlansByLineId(int lineId);
    Task<Production_Plan?> GetProdPlanById(int id);

    //-------------------------Reasons
    Task<List<Reason>?> GetReasons(int? id, string? description);
    Task<Reason?> GetReasonById(int id);

    //-------------------------Schedules
    Task<List<Schedule_Worker_Line>?> GetSchedules(int? id, DateTime? day, int? shift, int? lineId, int? operatorId, int? supervisorId);
    Task<List<Schedule_Worker_Line>?> GetSchedulesByOperatorId(int operatorId);
    Task<List<Schedule_Worker_Line>?> GetSchedulesBySupervisorId(int SupervisorId);

    //-------------------------Stops
    Task<List<Stop>?> GetStops(int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId);
    Task<List<Stop>?> GetStopsByLineId(int lineId);

    //-------------------------Supervisors
    Task<List<Supervisor>?> GetSupervisors(int? id, int? workerId);
    Task<Supervisor?> GetSupervisorByWorkerId(int workerId);

    //-------------------------Workers
    Task<List<Worker>?> GetWorkers(int? id, string? idFirebase, string? username, string? email, int? role);
    Task<Worker?> GetWorkerByIdFirebase(string idFirebase);
    Task<Worker?> GetWorkerById(int id);

    //-------------------------ComponentProducts
    Task<List<ComponentProduct>?> GetComponentProducts(int? id, int? componentId, int? productId, int? quantidade);
    Task<List<ComponentProduct>?> GetComponentProductsByProductId(int productId);
}
