using Context_aware_System.Controllers;
using ContextServer.Data;
using ContextServer.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.FakeItEasy;
using Models.ContextModels;
using Models.CustomModels;
using Models.FunctionModels;
using Moq;
using Newtonsoft.Json;
using Services.DataServices;
using System.Linq;
using System.Linq.Expressions;

namespace Testes
{
    public class ContextServerTests
    {

        //----------DeviceInfo---------------
        [Fact]
        public void DeviceInfo_Test_DeviceNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeDevices = new List<Device>
            {
                new Device { Id = 1, Type = 1 , LineId = 1},
                new Device { Id = 2, Type = 2 , LineId = 1},
                new Device { Id = 3, Type = 3 , LineId = 1},
            };

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(4).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(response);
            var rdi = (response as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal("Erro ao identificar o Device", rdi.Message);
        }

        [Fact]
        public void DeviceInfo_Test_LineNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(response);
            var rdi = (response as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(0, rdi.Line.Id);
            Assert.Equal("Erro ao identificar a line", rdi.Message);
        }

        [Fact]
        public void DeviceInfo_Is_Coordinator_CoordinatorNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator = new DataGenerator();
            //device é type 2 para ser do tipo coordinator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 2 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 2, WorkerId = 2},
               new Coordinator { Id = 3, WorkerId = 3},
            };
            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(response);
            var rdi = (response as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal("Erro ao identificar o coordinador", rdi.Message);
        }

        [Fact]
        public void DeviceInfo_Is_Coordinator_Worker_NotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            //device é type 2 para ser do tipo coordinator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 2 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };

            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(response);
            var rdi = (response as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal(1, rdi.Coordinator.Id);
            Assert.Equal(0, rdi.Worker.Id);
            Assert.Equal("Erro ao identificar o worker!!", rdi.Message);
        }

        [Fact]
        public void DeviceInfo_Is_Coordinator_OK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            //device é type 2 para ser do tipo coordinator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 2 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 1},
            };

            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1},
            };

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response);
            var rdi = (response as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal(1, rdi.Coordinator.Id);
            Assert.Equal(1, rdi.Worker.Id);
            Assert.Equal(2, rdi.listResponsavelLines.Count);
            Assert.Equal("Info obtida com sucesso!!", rdi.Message);
        }


        [Fact]
        public void DeviceInfo_Is_Operator_OK_There_Is_No_ProdPlan()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            //device é type 1 para ser do tipo operator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                };

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response);
            var rdi = (response as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal("Weareble-Operator", rdi.Type);
            Assert.Equal("A linha não tem nenhum plano de produção ativo no momento", rdi.Message);

        }

        [Fact]
        public void DeviceInfo_Is_Operator_Not_Found_Error_Finding_Product()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //device é type 1 para ser do tipo operator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1)
                   ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            //não existe o productid = 1
            generator.fakeProducts = new List<Product>
                {
                   new Product { Id = 2, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };
            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(response);
            var rdi = (response as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal("Weareble-Operator", rdi.Type);
            Assert.Equal("Erro ao identificar o Produto", rdi.Message);

        }

        [Fact]
        public void DeviceInfo_Is_Operator_OK_There_Is_No_MissingComponents()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //device é type 1 para ser do tipo operator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1)
                   ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            generator.fakeProducts = new List<Product>
                {
                   new Product { Id = 1, Name = "Product1" ,LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };

            generator.fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 2 ,ComponentId = 3, OrderDate = DateTime.Now.AddDays(-1)}
                };
            generator.fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 3, OrderDate = DateTime.Now.AddDays(-1)}
                };

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response);
            var rdi = (response as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal("Product1", rdi.ProductName);
            Assert.Equal("prod1", rdi.TagReference);
            Assert.Equal("Weareble-Operator", rdi.Type);
            Assert.Equal("Info obtida com sucesso!!", rdi.Message);
            Assert.Empty(rdi.listMissingComponentes);
        }

        [Fact]
        public void DeviceInfo_Is_Operator_OK_There_Is_MissingComponents()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //device é type 1 para ser do tipo operator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            //device é type 1 para ser do tipo operator
            generator.fakeDevices = new List<Device>
            {
               new Device { Id = 1, Type = 1 , LineId = 1},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1)
                   ,EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            generator.fakeProducts = new List<Product>
                {
                   new Product { Id = 1, Name = "Product1" ,LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };

            generator.fakeMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            generator.fakeComponents = new List<Component>
            {
               new Component { Id = 1, Name = "Component1", Reference = "Comp1", Category = 1},
            };

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response);
            var rdi = (response as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi.Line.Id);
            Assert.Equal("Product1", rdi.ProductName);
            Assert.Equal("prod1", rdi.TagReference);
            Assert.Equal("Weareble-Operator", rdi.Type);
            Assert.Equal("Info obtida com sucesso!!", rdi.Message);
            Assert.Single(rdi.listMissingComponentes);
        }

        //-------------------------------Operatorinfo
        [Fact]
        public void OperatorInfo_Worker_NotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            
            var response = controller.OperatorInfo("asdadaad").GetAwaiter().GetResult();
            
            Assert.IsType<NotFoundObjectResult>(response);
            var roi = (response as NotFoundObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi);
            Assert.Equal("Erro ao identificar o worker!!", roi.Message);
        }

        [Fact]
        public void OperatorInfo_Operator_NotFound()
        {                       
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1 },
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            generator.fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 2},
            };

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetOperatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetOperatorByWorkerId(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.OperatorInfo("pmfirebase").GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(response);
            var roi = (response as NotFoundObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi);
            Assert.Equal("Erro ao identificar o Operator!!", roi.Message);
            Assert.Equal(1, roi.Worker.Id);
        }

        [Fact]
        public void OperatorInfo_Ok_There_Is_Working_Schedule()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1 },
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            generator.fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 1},
            };
            generator.fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>
            {
               new Schedule_Worker_Line { Id = 1, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 2, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 1},
               //new Schedule_Worker_Line { Id = 3, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
            };

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetOperatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetOperatorByWorkerId(id));
            _dataService.Setup(x => x.GetSchedulesByOperatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesByOperatorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.OperatorInfo("pmfirebase").GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var roi = (response as OkObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi);
            Assert.Equal("O operador Pedro Monteiro está a trabalhar em 0 linha hoje", roi.Message);
            Assert.Equal(1, roi.Worker.Id);
            Assert.Equal(1, roi.Operator.Id);

        }

        [Fact]
        public void OperatorInfo_Ok_There_Is_No_Working_Schedule()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "pmfirebase", UserName = "Pedro Monteiro", Email = "pm@gmail.com", Role = 1 },
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            generator.fakeOperators = new List<Operator>
            {
               new Operator { Id = 1, WorkerId = 1},
            };
            generator.fakeSchedule_Worker_Lines = new List<Schedule_Worker_Line>
            {
               new Schedule_Worker_Line { Id = 1, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 2, Day = new DateTime(2023, 6, 28, 0, 0, 0), Shift = 2, LineId = 1, OperatorId = null, SupervisorId = 1},
               new Schedule_Worker_Line { Id = 3, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
               new Schedule_Worker_Line { Id = 4, Day = DateTime.Now.Date, Shift = 2, LineId = 1, OperatorId = 1, SupervisorId = null},
            };
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
            };

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetOperatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetOperatorByWorkerId(id));
            _dataService.Setup(x => x.GetSchedulesByOperatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesByOperatorId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.OperatorInfo("pmfirebase").GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var roi = (response as OkObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi);
            Assert.Equal("O operador Pedro Monteiro está a trabalhar em 1 linha hoje", roi.Message);
            Assert.Equal(1, roi.Worker.Id);
            Assert.Equal(1, roi.Operator.Id);
            Assert.Single(roi.listLine);
            Assert.Equal(2, roi.listSWL.Count);
        }

        //-------------------------------------------NewStopsInfo
        [Fact]
        public void NewStopsInfo_Ok_There_Are_no_Stops()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            generator.fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
               new Stop { Id = 2, Planned = true, InitialDate = new DateTime(2023, 6, 28, 12, 0, 0), EndDate = new DateTime(2023, 6, 28, 13, 30, 0),
                   Duration =  new TimeSpan(1, 30, 0), Shift = 2, LineId = 1, ReasonId = 2},
            };
            generator.fakeReasons = new List<Reason>
            {
               new Reason { Id = 1, Description = "Erro na Maquina 32" },
               new Reason { Id = 2, Description = "Hora de almoço" },
            };

            //trocar os dados do dataServices pelos do datagenerator           
            _dataService.Setup(x => x.GetStops(
            It.IsAny<int?>(),
            It.IsAny<bool?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<TimeSpan?>(),
            It.IsAny<int?>(),
            It.IsAny<int?>(),
            It.IsAny<int?>()
            ))
           .ReturnsAsync((int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId) =>
            {
            return generator.fakeStops;
            });
            _dataService.Setup(x => x.GetReasonById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetReasonById(id));


            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.StopsInfo(new DateTime(2030, 6, 29, 0, 0, 0), null, null).GetAwaiter().GetResult();

            //testes
            Assert.IsType<OkObjectResult>(response);
            var rsi = (response as OkObjectResult)?.Value as ResponseStopsInfo;
            Assert.NotNull(rsi);
            Assert.Equal("Não existem paragens nessas datas", rsi.Message);
        }

        [Fact]
        public void NewStopsInfo_OK_There_Are_Stops()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            generator.fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
            };
            generator.fakeReasons = new List<Reason>
            {
               new Reason { Id = 1, Description = "Erro na Maquina 32" },
               new Reason { Id = 2, Description = "Hora de almoço" },
            };

            //trocar os dados do dataServices pelos do datagenerator           
            _dataService.Setup(x => x.GetStops(
            It.IsAny<int?>(),
            It.IsAny<bool?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<DateTime?>(),
            It.IsAny<TimeSpan?>(),
            It.IsAny<int?>(),
            It.IsAny<int?>(),
            It.IsAny<int?>()
            ))
           .ReturnsAsync((int? id, bool? planned, DateTime? initialDate, DateTime? endDate, TimeSpan? duration, int? shift, int? lineId, int? reasonId) =>
           {
               return generator.fakeStops;
           });
            _dataService.Setup(x => x.GetReasonById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetReasonById(id));


            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.StopsInfo(new DateTime(2023, 6, 27, 0, 0, 0), null, null).GetAwaiter().GetResult();

            //testes
            Assert.IsType<OkObjectResult>(response);
            var rsi = (response as OkObjectResult)?.Value as ResponseStopsInfo;
            Assert.NotNull(rsi);
            Assert.Equal("Info obtida com sucesso", rsi.Message);
            var listStopsResponse = rsi.listNewStops;
            var stopResponse = listStopsResponse.SingleOrDefault();
            Assert.NotNull(stopResponse);
            Assert.Equal("Erro na Maquina 32", stopResponse.Description);
        }

        //-------------------------------------------LineInfo

        [Fact]
        public void LineInfo_Line_NotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.LineInfo(1, null, null).GetAwaiter().GetResult();
            
            //Testes
            Assert.IsType<NotFoundObjectResult>(response);
            var rli = (response as NotFoundObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli);
            Assert.Equal("Erro ao identificar a Line!!", rli.Message);
        }

        [Fact]
        public void LineInfo_CoordinatorNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 2, WorkerId = 2},
               new Coordinator { Id = 3, WorkerId = 3},
            };

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            //Testes
            Assert.IsType<NotFoundObjectResult>(response);
            var rli = (response as NotFoundObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli);
            Assert.Equal("Erro ao identificar o coordinator!!", rli.Message);
            Assert.Equal(1, rli.Line.Id);

        }

        [Fact]
        public void LineInfo_Worker_NotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            //Testes
            Assert.IsType<NotFoundObjectResult>(response);
            var rli = (response as NotFoundObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli);
            Assert.Equal("Erro ao identificar o worker!!", rli.Message);
            Assert.Equal(1, rli.Line.Id);
            Assert.Equal(1, rli.Coordinator.Id);
        }

        [Fact]
        public void LineInfo_OK_Test_Stops()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            generator.fakeStops = new List<Stop>
            {
               new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
               new Stop { Id = 2, Planned = false, InitialDate = new DateTime(2023, 6, 30, 10, 0, 0), EndDate = new DateTime(2023, 6, 30, 10, 15, 0),
                   Duration =  new TimeSpan(0, 15, 0), Shift = 2, LineId = 1, ReasonId = 1},
            };

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));

            //Inicializar Controllador com as Mocks

            //Teste1
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.LineInfo(1, new DateTime(2023, 7, 30, 10, 0, 0), null).GetAwaiter().GetResult();

            //Testes
            Assert.IsType<OkObjectResult>(response);
            var rli = (response as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli);
            Assert.Equal("Não existem Planos de produções nessas datas", rli.Message);
            Assert.Equal(1, rli.Line.Id);
            Assert.Equal(1, rli.Coordinator.Id);
            Assert.Equal(1, rli.Worker.Id);
            Assert.False(rli.listStops.Any());

            //Teste2
            var response2 = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response2);
            var rli2 = (response2 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli2);
            Assert.Equal("Não existem Planos de produções nessas datas", rli2.Message);
            Assert.Equal(1, rli2.Line.Id);
            Assert.Equal(1, rli2.Coordinator.Id);
            Assert.Equal(1, rli2.Worker.Id);
            Assert.Equal(2,rli2.listStops.Count);

            //Teste3
            var response3 = controller.LineInfo(1, new DateTime(2023, 6, 28, 10, 0, 0), new DateTime(2023, 6, 29, 10, 0, 0)).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response3);
            var rli3 = (response3 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli3);
            Assert.Equal("Não existem Planos de produções nessas datas", rli3.Message);
            Assert.Equal(1, rli3.Line.Id);
            Assert.Equal(1, rli3.Coordinator.Id);
            Assert.Equal(1, rli3.Worker.Id);
            Assert.Equal(1, rli3.listStops.Count);
        }

        [Fact]
        public void LineInfo_OK_Test_Production_Plans_Response()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 1},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeCoordinators = new List<Coordinator>
            {
               new Coordinator { Id = 1, WorkerId = 1},
            };

            generator.fakeWorkers = new List<Worker>
            {
               new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 2, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2},
               new Worker { Id = 3, IdFirebase = "rfirebase", UserName = "Rodrigo", Email = "r@gmail.com", Role = 3},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 2},
             };
            generator.fakeProducts = new List<Product>
                {
                   new Product { Id = 1, Name = "Product1" ,LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };
            generator.fakeProductions = new List<Production>
            {
               new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1},
               new Production { Id = 2, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1}
            };

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks

            //Teste1 - Não existe plano de produção
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var rli = (response as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli);
            Assert.Equal("Não existem Planos de produções nessas datas", rli.Message);
            Assert.Equal(1, rli.Line.Id);
            Assert.Equal(1, rli.Coordinator.Id);
            Assert.Equal(1, rli.Worker.Id);
            Assert.False(rli.listStops.Any());
            var ppr = rli.listProductionsByProductionPlan.FirstOrDefault();
            Assert.Null(ppr);


            //Teste2 - Existe plano de produção
            var controller2 = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response2 = controller.LineInfo(2, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response2);
            var rli2 = (response2 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli2);
            Assert.Equal("Info obtida com sucesso", rli2.Message);
            Assert.Equal(2, rli2.Line.Id);
            Assert.Equal(1, rli2.Coordinator.Id);
            Assert.Equal(1, rli2.Worker.Id);
            Assert.False(rli2.listStops.Any());
            var ppr2 = rli2.listProductionsByProductionPlan.FirstOrDefault();
            Assert.NotNull(ppr2);
            Assert.Equal("Plano de produção 1", ppr2.Production_plan.Name);
            Assert.Equal("Product1", ppr2.Product.Name);
            Assert.Equal(2, ppr2.listProductions.Count);
        }

        //-------------------------------------------SupervisorInfo
        //[Fact]
        //public async Task SupervisorInfo_Worker_NotFound()
        //{
        //    //test1
        //    var response = await controller.SupervisorInfo("dsadasads", null);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseSupervisorInfo;

        //    Assert.Equal("Erro ao identificar o worker!!", rdi.Message);
        //}

        //[Fact]
        //public async Task SupervisorInfo_Supervisor_NotFound()
        //{
        //    //test1
        //    var response = await controller.SupervisorInfo("hafirebase", null);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseSupervisorInfo;

        //    Assert.Equal("Erro ao identificar o Supervisor!!", rdi.Message);
        //}

        //[Fact]
        //public async Task SupervisorInfo_OK()
        //{
        //    //test1
        //    var response = await controller.SupervisorInfo("rfirebase", null);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseSupervisorInfo;

        //    Assert.NotNull(rdi.Supervisor);
        //}

        ////GetProductionsInfo
        //[Fact]
        //public async Task GetProductionsInfo_OK_Withproductions()
        //{
        //    //test1
        //    var response = await controller.GetProductionsInfo(1, null, null);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseGetProductionsInfo;

        //    Assert.Equal("Info obtida com sucesso!!", rdi.Message);
        //}

        //[Fact]
        //public async Task GetProductionsInfo_OK_WithoutProductions()
        //{
        //    //test1
        //    var response = await controller.GetProductionsInfo(10, null, null);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseGetProductionsInfo;

        //    Assert.Equal("Não encontrou produções nessa linha!!", rdi.Message);
        //}

        ////GetComponentsDeviceInfo
        //[Fact]
        //public async Task GetComponentsDeviceInfo_Device_NotFound()
        //{
        //    //test1
        //    var response = await controller.GetComponentsDeviceInfo(200);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseGetComponentsDeviceInfo;

        //    Assert.Equal("Erro ao identificar o Device!!", rdi.Message);
        //}

        //[Fact]
        //public async Task GetComponentsDeviceInfo_ProductionLine_NotFound()
        //{
        //    //test1
        //    var response = await controller.GetComponentsDeviceInfo(20);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseGetComponentsDeviceInfo;

        //    Assert.Equal("Erro ao identificar a linha de produção!!", rdi.Message);
        //}

        //[Fact]
        //public async Task GetComponentsDeviceInfo_ProductionPlan_NotFound()
        //{
        //    //test1
        //    var response = await controller.GetComponentsDeviceInfo(21);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseGetComponentsDeviceInfo;

        //    Assert.Equal("O device não se encontra em nenhuma linha no momento!!", rdi.Message);
        //}

        //[Fact]
        //public async Task GetComponentsDeviceInfo_Product_NotFound()
        //{
        //    //test1
        //    var response = await controller.GetComponentsDeviceInfo(22);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseGetComponentsDeviceInfo;

        //    Assert.Equal("Erro ao identificar a product!!", rdi.Message);
        //}

        //[Fact]
        //public async Task GetComponentsDeviceInfo_Ok()
        //{
        //    //test1
        //    var response = await controller.GetComponentsDeviceInfo(23);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseGetComponentsDeviceInfo;

        //    Assert.Equal("Info obtida com sucesso!!", rdi.Message);
        //}

        ////Product Info
        //[Fact]
        //public async Task ProductInfo_Line_NotFound()
        //{
        //    //test1
        //    var response = await controller.ProductInfo(200);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseProductInfo;

        //    Assert.Equal("Erro ao identificar a Line!!", rdi.Message);
        //}

        //[Fact]
        //public async Task ProductInfo_ProductionPlan_NotFound()
        //{
        //    //test1
        //    var response = await controller.ProductInfo(2);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseProductInfo;

        //    Assert.Equal("Não existe planos de produção na linha no momento!!", rdi.Message);
        //}

        //[Fact]
        //public async Task ProductInfo_Product_NotFound()
        //{
        //    //test1
        //    var response = await controller.ProductInfo(22);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseProductInfo;

        //    Assert.Equal("Erro ao identificar a product!!", rdi.Message);
        //}

        //[Fact]
        //public async Task ProductInfo_OK()
        //{
        //    //test1
        //    var response = await controller.ProductInfo(23);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseProductInfo;

        //    Assert.NotNull(rdi.Product);
        //}

        ////CoordinatorInfo
        //[Fact]
        //public async Task CoordinatorInfo_Worker_NotFound()
        //{
        //    //test1
        //    var response = await controller.CoordinatorInfo("adsgjasdh");
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseCoordinatorInfo;

        //    Assert.Equal("Erro ao identificar o worker!!", rdi.Message);
        //}

        //[Fact]
        //public async Task CoordinatorInfo_Coordinator_NotFound()
        //{
        //    //test1
        //    var response = await controller.CoordinatorInfo("hafirebase");
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseCoordinatorInfo;

        //    Assert.Equal("Erro ao identificar o Coordinator!!", rdi.Message);
        //}

        //[Fact]
        //public async Task CoordinatorInfo_Ok()
        //{
        //    //test1
        //    var response = await controller.CoordinatorInfo("pmfirebase");
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseCoordinatorInfo;

        //    Assert.Equal("Info obtida com sucesso!!", rdi.Message);
        //}
    }
}

