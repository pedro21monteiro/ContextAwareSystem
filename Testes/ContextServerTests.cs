using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Models.CustomModels;
using Moq;
using Newtonsoft.Json;
using System.Linq;

namespace Testes
{
    public class ContextServerTests
    {
        //Mock<IContextAwareDb> context;
        //SystemLogic systemLogic;
        //ContextAwareController controller;
        //DataService dataService;

        //public ContextServerTests()
        //{
        //    //Arrange
        //    context = new Mock<IContextAwareDb>();
        //    systemLogic = new SystemLogic();
        //    dataService = new DataService();

        //    controller = new ContextAwareController(context.Object, systemLogic,dataService);
        //    //moks
        //    var mockComponets = DbSetMock.CreateFrom(DataGenerator.GetFakeComponentes());
        //    var mockCoordinators = DbSetMock.CreateFrom(DataGenerator.GetFakeCoordinators());
        //    var mockDevices = DbSetMock.CreateFrom(DataGenerator.GetFakeDevices());
        //    var mockLines = DbSetMock.CreateFrom(DataGenerator.GetFakeLines());
        //    var mockOperators = DbSetMock.CreateFrom(DataGenerator.GetFakeOperators());
        //    var mockProducts = DbSetMock.CreateFrom(DataGenerator.GetFakeProducts());
        //    var mockProductions = DbSetMock.CreateFrom(DataGenerator.GetFakeProductions());
        //    var mockProduction_Plans = DbSetMock.CreateFrom(DataGenerator.GetFakeProduction_Plans());
        //    var mockReasons = DbSetMock.CreateFrom(DataGenerator.GetFakeReasons());
        //    var mockSchedules = DbSetMock.CreateFrom(DataGenerator.GetFakeSchedule_Worker_Lines());
        //    var mockStops = DbSetMock.CreateFrom(DataGenerator.GetFakeStops());
        //    var mockSupervisors = DbSetMock.CreateFrom(DataGenerator.GetFakeSupervisors());
        //    var mockWorkers = DbSetMock.CreateFrom(DataGenerator.GetFakeWorkers());
        //    //trocar o contexto pelo do datagenerator
        //    context.Setup(x => x.Components).Returns(mockComponets.Object);
        //    context.Setup(x => x.Coordinators).Returns(mockCoordinators.Object);
        //    context.Setup(x => x.Devices).Returns(mockDevices.Object);
        //    context.Setup(x => x.Lines).Returns(mockLines.Object);
        //    context.Setup(x => x.Operators).Returns(mockOperators.Object);
        //    context.Setup(x => x.Products).Returns(mockProducts.Object);
        //    context.Setup(x => x.Productions).Returns(mockProductions.Object);
        //    context.Setup(x => x.Production_Plans).Returns(mockProduction_Plans.Object);
        //    context.Setup(x => x.Reasons).Returns(mockReasons.Object);
        //    context.Setup(x => x.Schedule_Worker_Lines).Returns(mockSchedules.Object);
        //    context.Setup(x => x.Stops).Returns(mockStops.Object);
        //    context.Setup(x => x.Supervisors).Returns(mockSupervisors.Object);
        //    context.Setup(x => x.Workers).Returns(mockWorkers.Object);
        //}
        ////----------DeviceInfo---------------
        //[Fact]
        //public async Task DeviceInfo_Test_DeviceNotFound()
        //{
        //    var response = await controller.DeviceInfo(10);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseDeviceInfo;
        //    Assert.Equal("Erro ao identificar o Device!!", rdi.Message);
        //}

        //[Fact]
        //public async Task DeviceInfo_Test_LineNotFound()
        //{
        //    var response = await controller.DeviceInfo(4);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseDeviceInfo;
        //    Assert.Equal("Erro ao identificar a linha de produção!!", rdi.Message);
        //}

        //[Fact]
        //public async Task DeviceInfo_Test_Ok()
        //{
        //    var response = await controller.DeviceInfo(2);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseDeviceInfo;

        //    Assert.Equal("Info obtida com sucesso!!", rdi.Message);
        //}

        //[Fact]
        //public async Task DeviceInfo_CoordinatorNotFound()
        //{
        //    var response = await controller.DeviceInfo(5);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseDeviceInfo;

        //    Assert.Equal("Erro ao identificar o worker!!", rdi.Message);
        //}

        ////Operatorinfo
        //[Fact]
        //public async Task OperatorInfo_WorkerNotFound()
        //{
        //    //test1
        //    var response = await controller.OperatorInfo("asdadaad");
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseOperatorInfo;

        //    Assert.Equal("Erro ao identificar o worker!!", rdi.Message);
        //}

        //[Fact]
        //public async Task OperatorInfo_OperatorNotFound()
        //{
        //    //test1
        //    var response = await controller.OperatorInfo("pmfirebase");
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseOperatorInfo;

        //    Assert.Equal("Erro ao identificar o Operator!!", rdi.Message);
        //}

        //[Fact]
        //public async Task OperatorInfo_Ok()
        //{
        //    //test1
        //    var response = await controller.OperatorInfo("hafirebase");
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseOperatorInfo;

        //    Assert.Equal(1, rdi.Operator.Id);
        //}

        ////NewStopsInfo
        //[Fact]
        //public async Task NewStopsInfo_NotFound()
        //{
        //    //test1
        //    var response = await controller.StopsInfo(new DateTime(2030, 6, 29, 0, 0, 0), null, null);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseStopsInfo;

        //    Assert.Equal("Não existe paragens nessas datas!!", rdi.Message);
        //}

        ////NewStopsInfo
        //[Fact]
        //public async Task NewStopsInfo_OK()
        //{
        //    //test1
        //    var response = await controller.StopsInfo(null, new DateTime(2030, 6, 29, 0, 0, 0), null);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseStopsInfo;

        //    Assert.Equal("Info obtida com sucesso!!", rdi.Message);
        //}

        ////LineInfo
        //[Fact]
        //public async Task LineInfo_LineNotFound()
        //{
        //    //test1
        //    var response = await controller.LineInfo(100, null, null);
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseLineInfo;

        //    Assert.Equal("Erro ao identificar a Line!!", rdi.Message);
        //}

        //[Fact]
        //public async Task LineInfo_Production_PlansNotFound()
        //{
        //    //test1
        //    var response = await controller.LineInfo(1, null, new DateTime());
        //    // Assert
        //    var rdi = (response as NotFoundObjectResult).Value as ResponseLineInfo;

        //    Assert.Equal("Não existe planos de produção na linha no momento!!", rdi.Message);
        //}

        //[Fact]
        //public async Task LineInfo_Ok()
        //{
        //    //test1
        //    var response = await controller.LineInfo(15, null, null);
        //    // Assert
        //    var rdi = (response as OkObjectResult).Value as ResponseLineInfo;

        //    Assert.Equal("Info obtida com sucesso", rdi.Message);
        //}

        ////SupervisorInfo
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

