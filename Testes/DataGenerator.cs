using Microsoft.EntityFrameworkCore;
using MockQueryable.FakeItEasy;
using Models.ContextModels;
using Models.FunctionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class DataGenerator 
    {
        //Dados do dataServices
        public List<Component> fakeComponents = new List<Component>();
        public List<ComponentProduct> fakeComponentProducts = new List<ComponentProduct>();
        public List<Coordinator> fakeCoordinators = new List<Coordinator>();
        public List<Device> fakeDevices = new List<Device>();
        public List<Line> fakeLines = new List<Line>();
        public List<Operator> fakeOperators = new List<Operator>();
        public List<Product> fakeProducts = new List<Product>();
        public List<Production> fakeProductions = new List<Production>();
        public List<Production_Plan> fakeProduction_Plans = new List<Production_Plan>();
        public List<Reason> fakeReasons = new List<Reason>();
        public List<Schedule_Worker_Line> fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>();
        public List<Stop> fakeStops = new List<Stop>();
        public List<Supervisor> fakeSupervisors = new List<Supervisor>();
        public List<Worker> fakeWorkers = new List<Worker>();
        //Dados Context
        public List<MissingComponent> fakeMissingComponents = new List<MissingComponent>();
        public List<AlertsHistory> fakeAlertsHistories = new List<AlertsHistory>();


        //-----------------Implementar diferentes Cenários

        public void clearData()
        {
            fakeComponents.Clear();
            fakeComponentProducts.Clear();
            fakeCoordinators.Clear();
            fakeDevices.Clear();
            fakeLines.Clear();
            fakeOperators.Clear();
            fakeProducts.Clear();
            fakeProductions.Clear();
            fakeProduction_Plans.Clear();
            fakeReasons.Clear();
            fakeSchedule_Worker_Lines.Clear();
            fakeStops.Clear();
            fakeSupervisors.Clear();
            fakeWorkers.Clear();
            //Dados Context
            fakeMissingComponents.Clear();
            fakeAlertsHistories.Clear();
        }

        //---------Cenários Device Info

        public void TestDeviceInfo_Scenery_DeviceNotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
                new Device { Id = 1, Type = 1 , LineId = 1},
                new Device { Id = 2, Type = 2 , LineId = 1},
                new Device { Id = 3, Type = 3 , LineId = 1},
            };
        }

        public void TestDeviceInfo_Scenery_LineNotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
        }

        public void TestDeviceInfo_Scenery_Is_Coordinator_CoordinatorNotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 2 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 2, WorkerId = 2},
               new Coordinator { Id = 3, WorkerId = 3},
            };
        }

        public void TestDeviceInfo_Scenery_Is_Coordinator_Worker_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 2 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
        }

        public void TestDeviceInfo_Scenery_Is_Operator_Not_Found_Error_Finding_Product()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1)
                   ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            //não existe o productid = 1
            fakeProducts = new List<Product>
             {
                   new Product { Id = 2, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };
        }

        public void TestDeviceInfo_Scenery_Is_Coordinator_OK()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 2 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 1},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1},
            };
        }

        public void TestDeviceInfo_Scenery_Is_Operator_OK_There_Is_No_ProdPlan()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            fakeProduction_Plans = new List<Production_Plan>
            {
                new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
            };
        }

        public void TestDeviceInfo_Scenery_Is_Operator_OK_There_Is_No_MissingComponents()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1)
                   ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            fakeProducts = new List<Product>
                {
                   new Product { Id = 1, Name = "Product1" ,LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };

            fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 2 ,ComponentId = 3, OrderDate = DateTime.Now.AddDays(-1)}
                };
            fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 3, OrderDate = DateTime.Now.AddDays(-1)}
                };
        }

        public void TestDeviceInfo_Scenery_Is_Operator_OK_There_Is_MissingComponents()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            //device é type 1 para ser do tipo operator
            fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1)
                   ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            fakeProducts = new List<Product>
                {
                   new Product { Id = 1, Name = "Product1" ,LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };

            fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            fakeComponents = new List<Component>
            {
               new Component { Id = 1, Name = "Component1", Reference = "Comp1", Category = 1},
            };
        }

        //---------Cenários Operator Info

        public void TestOperatorInfo_Scenery_Worker_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
        }

        public void TestOperatorInfo_Scenery_Operator_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1 },
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 2},
            };
        }

        public void TestOperatorInfo_Scenery_Ok_There_Is_Working_Schedule()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1 },
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 1},
            };
            fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>
            {
               new Schedule_Worker_Line { Id = 1, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 2, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 1},
               new Schedule_Worker_Line { Id = 3, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 4, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };

        }

        public void TestOperatorInfo_Scenery_Ok_There_Is_No_Working_Schedule()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1 },
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 1},
            };
            fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>
            {
               new Schedule_Worker_Line { Id = 1, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 2, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 1},
               //new Schedule_Worker_Line { Id = 3, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
            };
        }

        //---------Cenários StopsInfo

        public void Test_NewStopsInfo_Scenery_Ok_There_Are_no_Stops()
        {
            clearData();
            //Implementar Cenário
            fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
               new Stop { Id = 2, Planned = true, InitialDate = new DateTime(2023, 6, 28, 12, 0, 0), EndDate = new DateTime(2023, 6, 28, 13, 30, 0),
                   Duration =  new TimeSpan(1, 30, 0), Shift = 2, LineId = 1, ReasonId = 2},
            };
            fakeReasons = new List<Reason>
            {
               new Reason { Id = 1, Description = "Erro na Maquina 32" },
               new Reason { Id = 2, Description = "Hora de almoço" },
            };
        }

        public void Test_NewStopsInfo_Scenery_Ok_There_Are_Stops()
        {
            clearData();
            //Implementar Cenário
            fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
            };
            fakeReasons = new List<Reason>
            {
               new Reason { Id = 1, Description = "Erro na Maquina 32" },
               new Reason { Id = 2, Description = "Hora de almoço" },
            };
        }

        //---------Cenários LinesInfo

        public void Test_LineInfo_Scenery_Line_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
        }

        public void Test_LineInfo_Scenery_CoordinatorNotFound()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 2, WorkerId = 2},
               new Coordinator { Id = 3, WorkerId = 3},
            };

        }

        public void Test_LineInfo_Scenery_Worker_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };

        }

        public void Test_LineInfo_Scenery_OK_Test_Stops()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
               new Stop { Id = 2, Planned = false, InitialDate = new DateTime(2023, 6, 30, 10, 0, 0), EndDate = new DateTime(2023, 6, 30, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
            };

        }

        public void Test_LineInfo_Scenery_OK_Test_Production_Plans_Response()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 1},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 2},
             };
            fakeProducts = new List<Product>
                {
                   new Product { Id = 1, Name = "Product1" ,LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };
            fakeProductions = new List<Production>
            {
               new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1},
               new Production { Id = 2, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1}
            };
        }


        //---------Cenários SupervisorInfo

        public void Test_SupervisorInfo_Scenery_Worker_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
        }

        public void Test_SupervisorInfo_Scenery_Supervisor_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "pefirebase", UserName = "Pedro", Email = "pedro@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3 },
            };
            fakeSupervisors = new List<Supervisor>
            {
               new Supervisor { Id = 1, WorkerId = 3 },
            };
        }

        public void Test_SupervisorInfo_Scenery_OK_Test_Schedules_And_Lines()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "pefirebase", UserName = "Pedro", Email = "pedro@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeSupervisors = new List<Supervisor>
            {
               new Supervisor { Id = 1, WorkerId = 3 },
            };
            fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>
            {
               new Schedule_Worker_Line { Id = 1, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
                new Schedule_Worker_Line { Id = 2, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 2},
                new Schedule_Worker_Line { Id = 2, Day = new DateTime(2023, 11, 21, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
        }


        //---------Cenários GetComponentsDeviceInfo


        public void Test_GetComponentsDeviceInfo_Scenery_Device_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
                new Device { Id = 1, Type = 1 , LineId = 1},
                new Device { Id = 2, Type = 2 , LineId = 1},
                new Device { Id = 3, Type = 3 , LineId = 1},
            };
        }

        public void Test_GetComponentsDeviceInfo_Scenery_Line_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
                new Device { Id = 1, Type = 1 , LineId = 1},
                new Device { Id = 2, Type = 2 , LineId = 1},
                new Device { Id = 3, Type = 3 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
        }
        public void Test_GetComponentsDeviceInfo_Scenery_ProductionPlan_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
                new Device { Id = 1, Type = 1 , LineId = 1},
                new Device { Id = 2, Type = 2 , LineId = 1},
                new Device { Id = 3, Type = 3 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 10},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   //new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
        }

        public void Test_GetComponentsDeviceInfo_Scenery_Product_Not_Found()
        {
            clearData();
            //Implementar Cenário
            fakeDevices = new List<Device>
            {
                new Device { Id = 1, Type = 1 , LineId = 1},
                new Device { Id = 2, Type = 2 , LineId = 1},
                new Device { Id = 3, Type = 3 , LineId = 1},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 10},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            fakeProducts = new List<Product>
                {
                   new Product { Id = 2, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };
        }

        //---------Cenários para testar ProductInfo
        public void Test_ProductInfo_Scenery_Line_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };

        }

        public void Test_ProductInfo_Scenery_Production_Plan_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
                new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   //new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
             };

        }

        public void Test_ProductInfo_Scenery_Product_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeLines = new List<Line>
            {
                new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            fakeProducts = new List<Product>
                {
                   new Product { Id = 2, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };


        }

        //----------------Cenários para testar CoordinatorInfo

        public void Test_CoordinatorInfo_Scenery_Worker_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
        }

        public void Test_CoordinatorInfo_Scenery_Coordinator_NotFound()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase2", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 6},
            };
        }

        public void Test_CoordinatorInfo_Scenery_OK_There_Is_No_Lines()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase2", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 3},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 4},
            };
        }

        public void Test_CoordinatorInfo_Scenery_OK_There_Is_Lines()
        {
            clearData();
            //Implementar Cenário
            fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase2", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 3},
            };
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
        }

        //Cenário para testar os missing Components
        public void TestGetMissingComponents_Scenery_OK()
        {
            clearData();
            //Implementar Cenário            
            fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };           

            fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            fakeComponents = new List<Component>
            {
               new Component { Id = 1, Name = "Component1", Reference = "Comp1", Category = 1},
            };
        }



        //------------------Funções dos services a testar

        public Device? GetDeviceById(int id)
        {
            return fakeDevices.FirstOrDefault(d => d.Id == id);
        }

        public Line? GetLineById(int id)
        {
            return fakeLines.FirstOrDefault(l => l.Id == id);
        }

        public Coordinator? GetCoordinatorById(int id)
        {
            return fakeCoordinators.FirstOrDefault(c => c.Id == id);
        }

        public Worker? GetWorkerById(int id)
        {
            return fakeWorkers.FirstOrDefault(w => w.Id == id);
        }
        public List<Line>? GetLinesByCoordinatorId(int coordinatorId)
        {
            return fakeLines.Where(w => w.CoordinatorId == coordinatorId).ToList();
        }

        public List<Production_Plan>? GetProdPlansByLineId(int lineId)
        {
            return fakeProduction_Plans.Where(p => p.LineId == lineId).ToList();
        }

        public Product? GetProductById(int id)
        {
            return fakeProducts.FirstOrDefault(p => p.Id == id);
        }

        public Component? GetComponentById(int id)
        {
            return fakeComponents.FirstOrDefault(c => c.Id == id);
        }

        public Worker? GetWorkerByIdFirebase(string id)
        {
            return fakeWorkers.FirstOrDefault(w => w.IdFirebase == id);
        }

        public Operator? GetOperatorByWorkerId(int id)
        {
            return fakeOperators.FirstOrDefault(w => w.WorkerId == id);
        }

        public List<Schedule_Worker_Line>? GetSchedulesByOperatorId(int id)
        {
            return fakeSchedule_Worker_Lines.Where(s => s.OperatorId == id).ToList();
        }

        public Reason? GetReasonById(int id)
        {
            return fakeReasons.Where(r => r.Id == id).FirstOrDefault();
        }

        public List<Stop>? GetStopsByLineId(int id)
        {
            return fakeStops.Where(s => s.LineId == id).ToList();
        }

        public List<Production>? GetProductionsByProdPlanId(int id)
        {
            return fakeProductions.Where(p => p.Production_PlanId == id).ToList();
        }

        public Supervisor? GetSupervisorByWorkerId(int id)
        {
            return fakeSupervisors.Where(s => s.WorkerId == id).FirstOrDefault();
        }
        public List<Schedule_Worker_Line>? GetSchedulesBySupervisorId(int id)
        {
            return fakeSchedule_Worker_Lines.Where(s => s.SupervisorId == id).ToList();
        }

        public List<ComponentProduct>? GetComponentProductsByProductId(int id)
        {
            return fakeComponentProducts.Where(c => c.ProductId == id).ToList();
        }

        public Coordinator? GetCoordinatorByWorkerId(int id)
        {
            return fakeCoordinators.Where(c => c.WorkerId == id).FirstOrDefault();
        }

        //----------Funções do context

        public List<MissingComponent> GetMissingComponents()
        {
            return fakeMissingComponents;
        }


    }
}
