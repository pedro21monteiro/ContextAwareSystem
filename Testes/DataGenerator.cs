using ContextServer.Data;
using ContextServer.Services;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes
{
    public static class DataGenerator 
    {
        //Componentes
        public static List<Component> GetFakeComponentes()
        {
            var fakeComponent = new List<Component>
            {
               new Component { Id = 1, Name = "Component1", Reference = "Comp1", Category = 1},
               new Component { Id = 2, Name = "Component2", Reference = "Comp1", Category = 1},
               new Component { Id = 3, Name = "Component3", Reference = "Comp1", Category = 1},
            };

            return fakeComponent;
        }

        //Coordinator
        public static List<Coordinator> GetFakeCoordinators()
        {
            var fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            return fakeCoordinators;
        }

        public static List<Device> GetFakeDevices()
        {
            var fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
               new Device { Id = 2, Type = 2 , LineId = 1},
               new Device { Id = 3, Type = 3 , LineId = 1},
               //esta linha não existe, é para testar
               new Device { Id = 4, Type = 3 , LineId = 10},
               new Device { Id = 5, Type = 3 , LineId = 3},
               //com uma linha de produção que não existe

               new Device { Id = 20, Type = 3 , LineId = 35},
               new Device { Id = 21, Type = 3 , LineId = 21},
               new Device { Id = 22, Type = 3 , LineId = 22},
               new Device { Id = 23, Type = 3 , LineId = 23}
            };

            return fakeDevices;
        }
        //lines
        public static List<Line> GetFakeLines()
        {
            var fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},


               new Line { Id = 15, Name = "Linha10" , Priority = true, CoordinatorId = 1},
               new Line { Id = 21, Name = "Linha22" , Priority = true, CoordinatorId = 1},
               new Line { Id = 22, Name = "Linha22" , Priority = true, CoordinatorId = 1},
               new Line { Id = 23, Name = "Linha23" , Priority = true, CoordinatorId = 1},
            };

            return fakeLines;
        }
        //operators
        public static List<Operator> GetFakeOperators()
        {
            var fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 2},
            };

            return fakeOperators;
        }
        //Product
        public static List<Product> GetFakeProducts()
        {
            //componentes do product 1
            List<Component> _components = new List<Component>();
            var fakeProducts = new List<Product>
            {
               new Product { Id = 1, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
            };

            return fakeProducts;
        }
        //ComponentProducts
        public static List<ComponentProduct> GetFakeComponentProducts()
        {
            var fakeComponentProducts = new List<ComponentProduct>
            {
               new ComponentProduct { Id = 1, ProductId = 1, ComponentId = 1, Quantidade = 1},
               new ComponentProduct { Id = 2, ProductId = 1, ComponentId = 2, Quantidade = 1},
            };

            return fakeComponentProducts;
        }

        //Productions
        public static List<Production> GetFakeProductions()
        {
            var fakeProductions = new List<Production>
            {
               new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1}
            };

            return fakeProductions;
        }

        //Production_Plans
        public static List<Production_Plan> GetFakeProduction_Plans()
        {
            var fakeProduction_Plans = new List<Production_Plan>
            {
               new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
               ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},

               //tenho de criar para testar ProductionNotFound um plano de produção que dé sempre
               new Production_Plan { Id = 20, Goal = 100 ,Name = "Plano de produção 2", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
               ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 15},

               //linha 22
               new Production_Plan { Id = 20, Goal = 100 ,Name = "Plano de produção 2", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
               ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 200, LineId = 22},
               //linha 23
               new Production_Plan { Id = 20, Goal = 100 ,Name = "Plano de produção 2", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
               ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 23}
            };
            return fakeProduction_Plans;
        }

        //Reasons
        public static List<Reason> GetFakeReasons()
        {
            var fakeReasons = new List<Reason>
            {
               new Reason { Id = 1, Description = "Erro na Maquina 32" },
               new Reason { Id = 2, Description = "Hora de almoço" },
            };

            return fakeReasons;
        }

        //Schedule_Worker_Line
        public static List<Schedule_Worker_Line> GetFakeSchedule_Worker_Lines()
        {
            var fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>
            {
               new Schedule_Worker_Line { Id = 1, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 2, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 1},
            };

            return fakeSchedule_Worker_Lines;
        }

        //Stops
        public static List<Stop> GetFakeStops()
        {
            var fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
               new Stop { Id = 2, Planned = true, InitialDate = new DateTime(2023, 6, 28, 12, 0, 0), EndDate = new DateTime(2023, 6, 28, 13, 30, 0),
                   Duration =  new TimeSpan(1, 30, 0), Shift = 2, LineId = 1, ReasonId = 2},
            };

            return fakeStops;
        }

        //Supervisors
        public static List<Supervisor> GetFakeSupervisors()
        {
            var fakeSupervisors = new List<Supervisor>
            {
               new Supervisor { Id = 1, WorkerId = 3 },
            };

            return fakeSupervisors;
        }

        //Workers
        public static List<Worker> GetFakeWorkers()
        {
            var fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            return fakeWorkers;
        }
    }
}
