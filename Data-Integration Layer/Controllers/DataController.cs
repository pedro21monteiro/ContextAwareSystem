using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Data_Integration_Layer.Controllers
{

    [Route("api/Data")]
    [ApiController]
    public class DataController : ControllerBase
    {

        //Neste controlador vai conter as informações que vai buscar a partir da camada de integração às BDs

        //-------------------------Informações relacionadas com os devices---------------------
        [HttpGet]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices()
        {
            List<Device> listDevices = new List<Device>()
            {
                new Device() { Id = 1, IdFirebase = "4eKGcqZnuQXVcq91M0RBZjZGTGq1",Type = 1, LineId = 1 },
            };

            return Ok(listDevices);
        }

        //-------------------------Informações relacionadas com os workers---------------------
        [HttpGet]
        [Route("GetWorkers")]
        public async Task<IActionResult> GetWorkers()
        {
            List<Worker> listWorkers = new List<Worker>()
            {
                new Worker() { Id = 1, IdFirebase = "MrWJm55GRLSqyx24ne1CGJ2YUr92",UserName ="Operador1",Email = "operador@lsi.com",Role = 2},
                new Worker() { Id = 2,  IdFirebase = "erfh76J9W7TChJPf07vDu3EGzVo2",UserName ="Supervisor1",Email = "supervisor@lsi.com",Role = 3},
                new Worker() { Id = 3,  IdFirebase = "AnREzGS07kOfhqs08dpBPuDXOsC2",UserName ="Coordenador1",Email = "coordenador@lsi.com",Role = 1},
               
            };


            return Ok(listWorkers);
        }

        //-------------------------Informações relacionadas com os Machines---------------------
        [HttpGet]
        [Route("GetMachines")]
        public async Task<IActionResult> GetMachines()
        {
            List<Machine> listMachines = new List<Machine>()
            {
                new Machine() { Id = 1, Name="Maquina de recorte",LineId = 1},
                new Machine() { Id = 2, Name="Maquina de Pesagem",LineId = 1},
                new Machine() { Id = 3, Name="Maquina 001",LineId = 1},
                new Machine() { Id = 4, Name="Maquina 002",LineId = 1},
                new Machine() { Id = 5, Name="Maquina 003",LineId = 2},
                new Machine() { Id = 6, Name="Maquina 004",LineId = 2},
                new Machine() { Id = 7, Name="Maquina 005",LineId = 2},
                new Machine() { Id = 8, Name="Maquina 006",LineId = 2},
                new Machine() { Id = 9, Name="Maquina 007",LineId = 3},
                new Machine() { Id = 10, Name="Maquina 008",LineId = 3},
                new Machine() { Id = 11, Name="Maquina 009",LineId = 3},
                new Machine() { Id = 12, Name="Maquina 010",LineId = 3},

                };


            return Ok(listMachines);
        }

        //-------------------------Informações relacionadas com os ProdutionLines---------------------
        [HttpGet]
        [Route("GetLines")]
        public async Task<IActionResult> GetLines()
        {
            List<Line> listLines = new List<Line>()
            {
                new Line() { Id = 1, Priority = true ,CoordenatorId= 3},
                new Line() { Id = 2, Priority = false ,CoordenatorId= 3},
                new Line() { Id = 3, Priority = false ,CoordenatorId= 3},

            };


            return Ok(listLines);
        }

        //-------------------------Informações relacionadas com os Stops---------------------
        [HttpGet]
        [Route("GetStops")]
        public async Task<IActionResult> GetStops()
        {
            List<Stop> listStops = new List<Stop>()
            {
                //paragens planeadas
                new Stop() { Id=1, LineId = 1, Planed = true, InitialDate = new DateTime(1, 1, 1, 12, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 14, 0, 0, 0)},
                new Stop() { Id=2, LineId = 1, Planed = true, InitialDate = new DateTime(1, 1, 1, 16, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 16, 30, 0, 0)},
                new Stop() { Id=3, LineId = 1, Planed = true, InitialDate = new DateTime(1, 1, 1, 10, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 10, 30, 0, 0)},
                new Stop() { Id=4, LineId = 2, Planed = true, InitialDate = new DateTime(1, 1, 1, 12, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 14, 0, 0, 0)},
                new Stop() { Id=5, LineId = 2, Planed = true, InitialDate = new DateTime(1, 1, 1, 16, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 16, 30, 0, 0)},
                new Stop() { Id=6, LineId = 2, Planed = true, InitialDate = new DateTime(1, 1, 1, 10, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 10, 30, 0, 0)},
                new Stop() { Id=7, LineId = 3, Planed = true, InitialDate = new DateTime(1, 1, 1, 12, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 14, 0, 0, 0)},
                new Stop() { Id=8, LineId = 3, Planed = true, InitialDate = new DateTime(1, 1, 1, 16, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 16, 30, 0, 0)},
                new Stop() { Id=9, LineId = 3, Planed = true, InitialDate = new DateTime(1, 1, 1, 10, 0, 0, 0),EndDate = new DateTime(1, 1, 1, 10, 30, 0, 0)},
                //paragens não planeadas
                new Stop() { Id=10, LineId = 1, Planed = false, InitialDate = new DateTime(2021, 12, 12, 12, 0, 0, 0),EndDate = new DateTime(2021, 12, 12, 14, 0, 0, 0)},
                new Stop() { Id=11, LineId = 2, Planed = false, InitialDate = new DateTime(2021, 11, 11, 16, 0, 0, 0),EndDate = new DateTime(2021, 11, 11, 16, 30, 0, 0)},
                new Stop() { Id=12, LineId = 3, Planed = false, InitialDate = new DateTime(2022, 9, 9, 10, 0, 0, 0),EndDate = new DateTime(2022, 9, 9, 10, 30, 0, 0)},
                 new Stop() { Id=12, LineId = 3, Planed = false, InitialDate = new DateTime(2023,3, 3, 10, 0, 0, 0),EndDate = new DateTime(2022, 9, 9, 10, 30, 0, 0)},
                };
            

            return Ok(listStops);
        }

        //-------------------------Informações relacionadas com as Producions---------------------

        [HttpGet]
        [Route("GetProductions")]
        public async Task<IActionResult> GetProductions()
        {
            List<Production> listProductions = new List<Production>()
            {
                new Production() { Id=1, Objective = 50 , Produced = 20, Order="Pl11111" ,LineId = 1, ProductId = 1, InitialDate = new DateTime(2021,10,11,0,0,0), EndDate = new DateTime(2023,10,11,0,0,0)},
                new Production() { Id=2, Objective = 30 , Produced = 21, Order="Pl22222" , LineId = 2, ProductId = 2, InitialDate = new DateTime(2022,1,1,0,0,0), EndDate = new DateTime(2022,8,1,0,0,0)},
                new Production() { Id=3, Objective = 30 , Produced = 11, Order="Pl33333" , LineId = 3, ProductId = 3, InitialDate = new DateTime(2022,1,1,0,0,0), EndDate = new DateTime(2023,12,12,0,0,0)},
                new Production() { Id=4, Objective = 30 , Produced = 11, Order="Pl44444" , LineId = 3, ProductId = 3, InitialDate = new DateTime(2023,1,11,12,0,0), EndDate = new DateTime(2023,1,15,12,0,0)},
            };


            return Ok(listProductions);
        }
        //-------------------------Informações relacionadas com as Producions---------------------

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            List<Product> listProducts = new List<Product>()
            {
                new Product() { Id=1, Reference="Referencia Antena 001",Name = "Antena 001"},
                new Product() { Id=2, Reference="Referencia Antena 002",Name = "Antena 002"},
                new Product() { Id=3, Reference="Referencia Antena 003",Name = "Antena 003"},

            };


            return Ok(listProducts);
        }

        //-------------------------Informações relacionadas com os Components---------------------

        [HttpGet]
        [Route("GetComponents")]
        public async Task<IActionResult> GetComponents()
        {
            List<Component> listComponents = new List<Component>()
            {
                new Component() { Id=1, Name="Parafuso1", Reference = "Componente 001",Category = 2},
                new Component() { Id=2, Name="Parafuso2",Reference = "Componente 002",Category = 2},
                new Component() { Id=3, Name="Parafuso3",Reference = "Componente 003",Category = 2},
                new Component() { Id=4, Name="Etiqueta1",Reference = "Componente 004",Category = 1},
                new Component() { Id=5, Name="Etiqueta2",Reference = "Componente 005",Category = 1},
                new Component() { Id=6, Name="Etiqueta3",Reference = "Componente 006",Category = 1},
                new Component() { Id=7, Name="Tabua 10 cm",Reference = "Componente 007",Category = 3},
                new Component() { Id=8, Name="Tabua 50 cm",Reference = "Componente 008",Category = 3},
                new Component() { Id=9, Name="Tabua 100 cm",Reference = "Componente 009",Category = 3},
                new Component() { Id=10, Name="Agrafos",Reference = "Componente 010",Category = 4},
                new Component() { Id=11, Name="Tecido",Reference = "Componente 011",Category = 5},
                new Component() { Id=12, Name="Ferro",Reference = "Componente 012",Category = 4},
                new Component() { Id=13, Name="Tijolo",Reference = "Componente 013",Category = 4},
                new Component() { Id=14, Name="Granito",Reference = "Componente 014",Category = 4},
                new Component() { Id=15, Name="Agua",Reference = "Componente 015",Category = 4},
                new Component() { Id=16, Name="Etiqueta4",Reference = "Componente 016",Category = 1},

            };


            return Ok(listComponents);
        }


        //-------------------------Informações relacionadas com os Product_Componets---------------------

        [HttpGet]
        [Route("GetProduct_Components")]
        public async Task<IActionResult> GetProduct_Components()
        {
            List<Product_Component> listProduct_Components = new List<Product_Component>()
            {
                 new Product_Component() { Id=1, ProductId = 1, ComponentId = 1, Quantity = 200},
                 new Product_Component() { Id=2, ProductId = 1, ComponentId = 4, Quantity = 200},
                 new Product_Component() { Id=3, ProductId = 1, ComponentId = 7, Quantity = 200},
                 new Product_Component() { Id=4, ProductId = 1, ComponentId = 10, Quantity = 200},
                 new Product_Component() { Id=5, ProductId = 1, ComponentId = 12, Quantity = 200},
                 new Product_Component() { Id=6, ProductId = 1, ComponentId = 14, Quantity = 200},
                 //--
                 new Product_Component() { Id=7, ProductId = 2, ComponentId = 2, Quantity = 100},
                 new Product_Component() { Id=8, ProductId = 2, ComponentId = 5, Quantity = 100},
                 new Product_Component() { Id=9, ProductId = 2, ComponentId = 8, Quantity = 100},
                 new Product_Component() { Id=10, ProductId = 2, ComponentId = 11, Quantity = 100},
                 new Product_Component() { Id=11, ProductId = 2, ComponentId = 13, Quantity = 100},
                 new Product_Component() { Id=12, ProductId = 2, ComponentId = 15, Quantity = 100},
                 //--
                 new Product_Component() { Id=13, ProductId = 3, ComponentId = 3, Quantity = 57},
                 new Product_Component() { Id=14, ProductId = 3, ComponentId = 6, Quantity = 57},
                 new Product_Component() { Id=15, ProductId = 3, ComponentId = 7, Quantity = 57},
                 new Product_Component() { Id=16, ProductId = 3, ComponentId = 9, Quantity = 57},
                 new Product_Component() { Id=17, ProductId = 3, ComponentId = 13, Quantity = 57},
                 new Product_Component() { Id=18, ProductId = 3, ComponentId = 14, Quantity = 57},

            };


            return Ok(listProduct_Components);
        }

        //-------------------------Informações relacionadas com os Operators---------------------

        [HttpGet]
        [Route("GetOperators")]
        public async Task<IActionResult> GetOperators()
        {
            List<Operator> listOperators = new List<Operator>()
            {
                new Operator(){WorkerId = 1},
            };

            return Ok(listOperators);
        }

        //-------------------------Informações relacionadas com os Coordinators---------------------

        [HttpGet]
        [Route("GetCoordinators")]
        public async Task<IActionResult> GetCoordinators()
        {
            List<Coordinator> listCoordinators = new List<Coordinator>()
            {
                new Coordinator(){WorkerId = 3},
            };

            return Ok(listCoordinators);
        }

        //-------------------------Informações relacionadas com as Reasons---------------------

        [HttpGet]
        [Route("GetReasons")]
        public async Task<IActionResult> GetReasons()
        {
            List<Reason> listReasons = new List<Reason>()
            {
                new Reason(){Id = 1,StopId = 1, Description = "Hora do almoço"},
                new Reason(){Id = 2,StopId = 2, Description = "Hora do lache"},
                new Reason(){Id = 3,StopId = 3, Description = "Hora do lanche da manha"},
                new Reason(){Id = 4,StopId = 4, Description = "Hora do almoço"},
                new Reason(){Id = 5,StopId = 5, Description = "Hora do lache"},
                new Reason(){Id = 6,StopId = 6, Description = "Hora do lanche da manha"},
                new Reason(){Id = 7,StopId = 7, Description = "Hora do almoço"},
                new Reason(){Id = 8,StopId = 8, Description = "Hora do lache"},
                new Reason(){Id = 9,StopId = 9, Description = "Hora do lanche da manha"},

                new Reason(){Id = 10,StopId = 10, Description = "Erro na maquina de pesagem", MachineId= 2},
                new Reason(){Id = 11,StopId = 11, Description = "Erro na maquina 004", MachineId= 6},
                new Reason(){Id = 12,StopId = 12, Description = "Erro na maquina 007", MachineId= 9},
                                            
            };

            return Ok(listReasons);
        }

        //-------------------------Informações relacionadas com as Schedule_Worker_Line---------------------

        [HttpGet]
        [Route("GetSchedule_Worker_Lines")]
        public async Task<IActionResult> GetSchedule_Worker_Lines()
        {
            List<Schedule_Worker_Line> listSchedule_Worker_Lines = new List<Schedule_Worker_Line>()
            {

                new Schedule_Worker_Line(){Id = 1, WorkerId = 1, LineId =1, InitialDate = new DateTime(2022, 12, 21, 0, 0, 0, 0), EndDate = new DateTime(2022, 12, 21, 23, 59, 59, 0)},
               
                new Schedule_Worker_Line(){Id = 2, WorkerId = 1, LineId =2, InitialDate = new DateTime(2022, 12, 22, 0, 0, 0, 0), EndDate = new DateTime(2022, 12, 22, 23, 59, 59, 0)},

                new Schedule_Worker_Line(){Id = 3, WorkerId = 2, LineId =2, InitialDate = new DateTime(2022, 12, 22, 0, 0, 0, 0), EndDate = new DateTime(2022, 12, 22, 23, 59, 59, 0)},

                new Schedule_Worker_Line(){Id = 4, WorkerId = 2, LineId =3, InitialDate = new DateTime(2022, 12, 23, 0, 0, 0, 0), EndDate = new DateTime(2022, 12, 23, 23, 59, 59, 0)},
                new Schedule_Worker_Line(){Id = 5, WorkerId =2, LineId =2, InitialDate = new DateTime(2023, 1, 16, 0, 0, 0, 0), EndDate = new DateTime(2023, 1, 16, 23, 59, 59, 0)},

                //supervisors
                new Schedule_Worker_Line(){Id = 6, WorkerId =2, LineId =2, InitialDate = new DateTime(2023, 1, 9, 0, 0, 0, 0), EndDate = new DateTime(2023, 1, 9, 23, 59, 59, 0)},
            };

            return Ok(listSchedule_Worker_Lines);
        }
        //-------------------------Informações relacionadas com os Supervisors---------------------

        [HttpGet]
        [Route("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors()
        {
            List<Supervisor> listSupervisors = new List<Supervisor>()
            {
                new Supervisor(){WorkerId = 2},
            };

            return Ok(listSupervisors);
        }

    }
}
