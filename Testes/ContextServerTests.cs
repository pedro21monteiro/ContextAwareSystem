﻿using Context_aware_System.Controllers;
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

namespace Tests
{
    public class ContextServerTests
    {
        //---------------------------------------DeviceInfo---------------------------------------

        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "NotFound" quando não é possivel identificar o dispositivo.
        /// </summary>
        [Fact]
        public void DeviceInfo_DeviceNotFound()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_DeviceNotFound();

            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));


            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste 1 - DeviceNotFound
            generator.TestDeviceInfo_Scenery_DeviceNotFound();
            var responseTest = controller.DeviceInfo(4).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<NotFoundObjectResult>(responseTest);
            var rdi = (responseTest as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal("Erro ao identificar o Device", rdi?.Message);
        }

        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "NotFound" quando não é possivel identificar a linha de produção
        ///  associada ao dispositivo.
        /// </summary>
        [Fact]
        public void DeviceInfo_LineNotFound()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_LineNotFound();

            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<NotFoundObjectResult>(responseTest);
            var rdi = (responseTest as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(0, rdi?.Line.Id);
            Assert.Equal("Erro ao identificar a line", rdi?.Message);
        }

        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "NotFound" quando o dispositivo está 
        ///  associado a um coordenador, e não é possível o identificar.
        /// </summary>
        [Fact]
        public void DeviceInfo_Is_Coordinator_CoordinatorNotFound()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_Is_Coordinator_CoordinatorNotFound();

            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<NotFoundObjectResult>(responseTest);
            var rdi = (responseTest as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi?.Line.Id);
            Assert.Equal("Erro ao identificar o coordinador", rdi?.Message);
        }

        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "NotFound" quando o dispositivo está 
        ///  associado a um coordenador, e não é possível indentificar as informações de trabalhador desse coordenador.
        /// </summary>
        [Fact]
        public void DeviceInfo_Is_Coordinator_WorkerNotFound()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_Is_Coordinator_Worker_NotFound();

            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert  

            Assert.IsType<NotFoundObjectResult>(responseTest);
            var rdi = (responseTest as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi?.Line.Id);
            Assert.Equal(1, rdi?.Coordinator.Id);
            Assert.Equal(0, rdi?.Worker.Id);
            Assert.Equal("Erro ao identificar o worker!!", rdi?.Message);
        }


        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "NotFound" quando o dispositivo está 
        ///  associado a um coordenador, e não é possível indentificar as informações de trabalhador desse coordenador.
        /// </summary>
        [Fact]
        public void DeviceInfo_Is_Operator_ProductNotFound()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_Is_Operator_Not_Found_Error_Finding_Product();

            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert  
            Assert.IsType<NotFoundObjectResult>(responseTest);
            var rdi = (responseTest as NotFoundObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi?.Line.Id);
            Assert.Equal("Weareble-Operator", rdi?.Type);
            Assert.Equal("Erro ao identificar o Produto", rdi?.Message);
        }


        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "Ok" quando o dispositivo está associado a um coordenador
        ///  e as informações são obtidas com sucesso.
        /// </summary>
        [Fact]
        public void DeviceInfo_Is_Coordinator_OK()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_Is_Coordinator_OK();

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

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert  
            Assert.IsType<OkObjectResult>(responseTest);
            var rdi = (responseTest as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi?.Line.Id);
            Assert.Equal(1, rdi?.Coordinator.Id);
            Assert.Equal(1, rdi?.Worker.Id);
            Assert.Equal(2, rdi?.listResponsavelLines.Count);
            Assert.Equal("Info obtida com sucesso!!", rdi?.Message);
        }

        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "Ok" em casos que a linha não tem planos de 
        ///  produção ativos, com a devida informação.
        /// </summary>
        [Fact]
        public void DeviceInfo_Is_Operator_OK_ThereIsNoProdPlan()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //inicializar o cenário
            generator.TestDeviceInfo_Scenery_Is_Operator_OK_There_Is_No_ProdPlan();

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

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert  
            Assert.IsType<OkObjectResult>(responseTest);
            var rdi = (responseTest as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi);
            Assert.Equal(1, rdi?.Line.Id);
            Assert.Equal("Weareble-Operator", rdi?.Type);
            Assert.Equal("A linha não tem nenhum plano de produção ativo no momento", rdi?.Message);
        }

        /// <summary>
        ///  Assegura que o método "DeviceInfo" retorna "Ok" em casos que a linha possa ter ou não componentes em falta.
        /// </summary>
        [Fact]
        public void DeviceInfo_Is_Operator_OK_TestMissingComponents()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();


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

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.DeviceInfo(1).GetAwaiter().GetResult();

            // Assert  
            //Teste 3 - Is_Operator_OK_There_Is_No_MissingComponents
            generator.TestDeviceInfo_Scenery_Is_Operator_OK_There_Is_No_MissingComponents();

            //trocar os dados do _context pelos do datagenerator
            fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var responseTest3 = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(responseTest3);
            var rdi3 = (responseTest3 as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi3);
            Assert.Equal(1, rdi3?.Line.Id);
            Assert.Equal("Product1", rdi3?.ProductName);
            Assert.Equal("prod1", rdi3?.TagReference);
            Assert.Equal("Weareble-Operator", rdi3?.Type);
            Assert.Equal("Info obtida com sucesso!!", rdi3?.Message);
            Assert.Empty(rdi3?.listMissingComponentes);

            //Teste 4 - Is_Operator_OK_There_Is_MissingComponents

            generator.TestDeviceInfo_Scenery_Is_Operator_OK_There_Is_MissingComponents();

            //trocar os dados do _context pelos do datagenerator
            fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);
            controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var responseTest4 = controller.DeviceInfo(1).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(responseTest4);
            var rdi4 = (responseTest4 as OkObjectResult)?.Value as ResponseDeviceInfo;
            Assert.NotNull(rdi4);
            Assert.Equal(1, rdi4?.Line.Id);
            Assert.Equal("Product1", rdi4?.ProductName);
            Assert.Equal("prod1", rdi4?.TagReference);
            Assert.Equal("Weareble-Operator", rdi4?.Type);
            Assert.Equal("Info obtida com sucesso!!", rdi4?.Message);
            Assert.Single(rdi4?.listMissingComponentes);
        }

        //---------------------------------------OperatorInfo---------------------------------------

        /// <summary>
        /// Assegura que o método retorne NotFound qunado não é possivel identificar o trabalhador.
        /// </summary>
        [Fact]
        public void OperatorInfo_WorkerNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetOperatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetOperatorByWorkerId(id));
            _dataService.Setup(x => x.GetSchedulesByOperatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesByOperatorId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste 1 - Worker_NotFound
            generator.TestOperatorInfo_Scenery_Worker_NotFound();
            var responseTest1 = controller.OperatorInfo("asdadaad").GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest1);
            var roi1 = (responseTest1 as NotFoundObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi1);
            Assert.Equal("Erro ao identificar o worker!!", roi1?.Message);

        }

        /// <summary>
        /// Assegura que o método retorne NotFound qunado não é possivel identificar o operador.
        /// </summary>
        [Fact]
        public void OperatorInfo_OperatorNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetOperatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetOperatorByWorkerId(id));
            _dataService.Setup(x => x.GetSchedulesByOperatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesByOperatorId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test 2 - Operator_NotFound
            generator.TestOperatorInfo_Scenery_Operator_NotFound();
            var responseTest2 = controller.OperatorInfo("pmfirebase").GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest2);
            var roi2 = (responseTest2 as NotFoundObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi2);
            Assert.Equal("Erro ao identificar o Operator!!", roi2?.Message);
            Assert.Equal(1, roi2?.Worker.Id);

        }

        /// <summary>
        /// Assegura que o método "OperatorInfo" retorna "Ok" para vários cenários válidos, testando funcionalidades 
        /// como a identificação de horários de trabalho e a linha em que o trabalhador está a atuar no dia.
        /// </summary>
        [Fact]
        public void OperatorInfo_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();
            //Atribuir valores ao dados que vou utilizar
            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetOperatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetOperatorByWorkerId(id));
            _dataService.Setup(x => x.GetSchedulesByOperatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesByOperatorId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test1 - There_Is_No_Working_Schedule
            generator.TestOperatorInfo_Scenery_Ok_There_Is_No_Working_Schedule();
            var responseTest1 = controller.OperatorInfo("pmfirebase").GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(responseTest1);
            var roi1 = (responseTest1 as OkObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi1);
            Assert.Equal("O operador Pedro Monteiro está a trabalhar em 0 linha hoje", roi1?.Message);
            Assert.Equal(1, roi1?.Worker.Id);
            Assert.Equal(1, roi1?.Operator.Id);


            //Test2 - There_Is_Working_Schedule
            generator.TestOperatorInfo_Scenery_Ok_There_Is_Working_Schedule();
            var responseTest2 = controller.OperatorInfo("pmfirebase").GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(responseTest2);
            var roi2 = (responseTest2 as OkObjectResult)?.Value as ResponseOperatorInfo;
            Assert.NotNull(roi2);
            Assert.Equal("O operador Pedro Monteiro está a trabalhar em 1 linha hoje", roi2?.Message);
            Assert.Equal(1, roi2?.Worker.Id);
            Assert.Equal(1, roi2?.Operator.Id);
            Assert.Single(roi2?.listLine);
            Assert.Equal(2, roi2?.listSWL.Count);
        }

        //---------------------------------------StopsInfo---------------------------------------

        /// <summary>
        /// Garante que o método "StopsInfo" devolve "OK" em vários cenários válidos. Este teste inclui a avaliação de 
        /// funcionalidades, como a disponibilização de informações sobre paragens e suas razões, além de verificar 
        /// situações em que não deve retornar qualquer paragem.
        /// </summary>
        [Fact]
        public void StopsInfo_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

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


            //Test1 - Ok_There_Are_no_Stops
            generator.Test_NewStopsInfo_Scenery_Ok_There_Are_no_Stops();
            var responseTest1 = controller.StopsInfo(new DateTime(2030, 6, 29, 0, 0, 0), null, null).GetAwaiter().GetResult();

            //testes
            Assert.IsType<OkObjectResult>(responseTest1);
            var rsi1 = (responseTest1 as OkObjectResult)?.Value as ResponseStopsInfo;
            Assert.NotNull(rsi1);
            Assert.Equal("Não existem paragens nessas datas", rsi1?.Message);

            //Test 2 - Ok_There_Are_Stops
            generator.Test_NewStopsInfo_Scenery_Ok_There_Are_Stops();
            var responseTest2 = controller.StopsInfo(new DateTime(2023, 6, 27, 0, 0, 0), null, null).GetAwaiter().GetResult();

            //testes
            Assert.IsType<OkObjectResult>(responseTest2);
            var rsi2 = (responseTest2 as OkObjectResult)?.Value as ResponseStopsInfo;
            Assert.NotNull(rsi2);
            Assert.Equal("Info obtida com sucesso", rsi2?.Message);
            var listStopsResponse = rsi2?.listNewStops;
            var stopResponse = listStopsResponse?.SingleOrDefault();
            Assert.NotNull(stopResponse);
            Assert.Equal("Erro na Maquina 32", stopResponse?.Description);
        }

        //---------------------------------------LineInfo---------------------------------------

        /// <summary>
        /// Assegura que o método "LineInfo" retorna "NotFound" quando não é possível identificar a linha de produção.
        /// </summary>
        [Fact]
        public void LineInfo_LineNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks

            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste1 - Line_NotFound
            generator.Test_LineInfo_Scenery_Line_NotFound();

            var responseTest1 = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest1);
            var rli1 = (responseTest1 as NotFoundObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli1);
            Assert.Equal("Erro ao identificar a Line!!", rli1?.Message);
        }

        /// <summary>
        /// Assegura que o método "LineInfo" retorna "NotFound" quando não é possível identificar 
        /// o coordenador da linha de produção.
        /// </summary>
        [Fact]
        public void LineInfo_CoordinatorNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks

            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);


            //Teste 2 - CoordinatorNotFound
            generator.Test_LineInfo_Scenery_CoordinatorNotFound();
            var responseTest2 = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest2);
            var rli2 = (responseTest2 as NotFoundObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli2);
            Assert.Equal("Erro ao identificar o coordinator!!", rli2?.Message);
            Assert.Equal(1, rli2?.Line.Id);

        }

        /// <summary>
        /// Assegura que o método "LineInfo" retorna "NotFound" quando não é possível identificar 
        /// o trabalhador responsável pela linha de produção.
        /// </summary>
        [Fact]
        public void LineInfo_WorkerNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks

            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste 3 - Worker_NotFound
            generator.Test_LineInfo_Scenery_Worker_NotFound();
            var responseTest3 = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest3);
            var rli3 = (responseTest3 as NotFoundObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rli3);
            Assert.Equal("Erro ao identificar o worker!!", rli3?.Message);
            Assert.Equal(1, rli3?.Line.Id);
            Assert.Equal(1, rli3?.Coordinator.Id);


        }

        /// <summary>
        /// Assegura que o método "LineInfo" retorna "OK" em diversos cenários válidos, e testa a resposta do mesmo
        /// relativamente às paragens.
        /// </summary>
        [Fact]
        public void LineInfo_ReturnsOK_TestStops()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks

            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste1 - TestStops
            generator.Test_LineInfo_Scenery_OK_Test_Stops();

            //Test 1.1 - Inserir data Inical
            var responseTest1_1 = controller.LineInfo(1, new DateTime(2023, 7, 30, 10, 0, 0), null).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(responseTest1_1);
            var rliTest1_1 = (responseTest1_1 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rliTest1_1);
            Assert.Equal("Não existem Planos de produções nessas datas", rliTest1_1?.Message);
            Assert.Equal(1, rliTest1_1?.Line.Id);
            Assert.Equal(1, rliTest1_1?.Coordinator.Id);
            Assert.Equal(1, rliTest1_1?.Worker.Id);
            Assert.False(rliTest1_1?.listStops.Any());

            //Test 1.2 - Não inserir datas
            var responseTest1_2 = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(responseTest1_2);
            var rliTest1_2 = (responseTest1_2 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rliTest1_2);
            Assert.Equal("Não existem Planos de produções nessas datas", rliTest1_2?.Message);
            Assert.Equal(1, rliTest1_2?.Line.Id);
            Assert.Equal(1, rliTest1_2?.Coordinator.Id);
            Assert.Equal(1, rliTest1_2?.Worker.Id);
            Assert.Equal(2, rliTest1_2?.listStops.Count);

            //Test 1.3 - Inserir data inicial e final
            var responseTest1_3 = controller.LineInfo(1, new DateTime(2023, 6, 28, 10, 0, 0), new DateTime(2023, 6, 29, 10, 0, 0)).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(responseTest1_3);
            var rliTest1_3 = (responseTest1_3 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rliTest1_3);
            Assert.Equal("Não existem Planos de produções nessas datas", rliTest1_3?.Message);
            Assert.Equal(1, rliTest1_3?.Line.Id);
            Assert.Equal(1, rliTest1_3?.Coordinator.Id);
            Assert.Equal(1, rliTest1_3?.Worker.Id);
            Assert.Equal(1, rliTest1_3?.listStops.Count);

        }

        /// <summary>
        /// Assegura que o método "LineInfo" retorna "OK" em diversos cenários válidos, e testa a resposta do mesmo
        /// relativamente aos planos de produção.
        /// </summary>
        [Fact]
        public void LineInfo_ReturnsOK_TestProductionPlans()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetCoordinatorById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorById(id));
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));
            _dataService.Setup(x => x.GetStopsByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetStopsByLineId(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks

            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste2 - Test_Production_Plans_Response
            generator.Test_LineInfo_Scenery_OK_Test_Production_Plans_Response();

            //Test2_1 - Não existe plano de produção
            var responseTest2_1 = controller.LineInfo(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(responseTest2_1);
            var rliTest2_1 = (responseTest2_1 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rliTest2_1);
            Assert.Equal("Não existem Planos de produções nessas datas", rliTest2_1?.Message);
            Assert.Equal(1, rliTest2_1?.Line.Id);
            Assert.Equal(1, rliTest2_1?.Coordinator.Id);
            Assert.Equal(1, rliTest2_1?.Worker.Id);
            Assert.False(rliTest2_1?.listStops.Any());
            var ppr = rliTest2_1?.listProductionsByProductionPlan.FirstOrDefault();
            Assert.Null(ppr);

            //Test2_2 - Existe plano de produção

            var responseTest2_2 = controller.LineInfo(2, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(responseTest2_2);
            var rliTest2_2 = (responseTest2_2 as OkObjectResult)?.Value as ResponseLineInfo;
            Assert.NotNull(rliTest2_2);
            Assert.Equal("Info obtida com sucesso", rliTest2_2?.Message);
            Assert.Equal(2, rliTest2_2?.Line.Id);
            Assert.Equal(1, rliTest2_2?.Coordinator.Id);
            Assert.Equal(1, rliTest2_2?.Worker.Id);
            Assert.False(rliTest2_2?.listStops.Any());
            var ppr2 = rliTest2_2?.listProductionsByProductionPlan.FirstOrDefault();
            Assert.NotNull(ppr2);
            Assert.Equal("Plano de produção 1", ppr2?.Production_plan.Name);
            Assert.Equal("Product1", ppr2?.Product.Name);
            Assert.Equal(2, ppr2?.listProductions.Count);
        }


        //---------------------------------------SupervisorInfo---------------------------------------

        /// <summary>
        /// Assegura que o método "SupervisorInfo" retorne "NotFound" quando não é possivel identificar o trabalhador.
        /// </summary>
        [Fact]
        public void SupervisorInfo_WorkerNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            // trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetSupervisorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSupervisorByWorkerId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetSchedulesBySupervisorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesBySupervisorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test1- Worker_NotFound
            generator.Test_SupervisorInfo_Scenery_Worker_NotFound();

            var responseTest1 = controller.SupervisorInfo("dsadasads", null).GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(responseTest1);
            var rsiTest1 = (responseTest1 as NotFoundObjectResult)?.Value as ResponseSupervisorInfo;
            Assert.NotNull(rsiTest1);
            Assert.Equal("Erro ao identificar o worker!!", rsiTest1?.Message);

        }

        /// <summary>
        /// Assegura que o método "SupervisorInfo" retorne "NotFound" quando não é possivel identificar o supervisor.
        /// </summary>
        [Fact]
        public void SupervisorInfo_SupervisorNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            // trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetSupervisorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSupervisorByWorkerId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetSchedulesBySupervisorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesBySupervisorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test2- Supervisor_NotFound
            generator.Test_SupervisorInfo_Scenery_Supervisor_NotFound();
            var responseTest2 = controller.SupervisorInfo("pefirebase", null).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest2);
            var rsiTest2 = (responseTest2 as NotFoundObjectResult)?.Value as ResponseSupervisorInfo;
            Assert.NotNull(rsiTest2);
            Assert.Equal(2, rsiTest2?.Worker.Id);
            Assert.Equal("Erro ao identificar o Supervisor!!", rsiTest2?.Message);

        } 

        /// <summary>
        /// Garante que o método "SupervisorInfo" retorna "OK" em diversos cenários válidos. Os testes incluem a inserção 
        /// de diferentes parâmetros para verificar a resposta do mesmo sobre informações como as linhas de produção pelas 
        /// quais o supervisor é responsável e seus horários de trabalho.
        /// </summary>
        [Fact]
        public void SupervisorInfo_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            // trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetSupervisorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSupervisorByWorkerId(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetSchedulesBySupervisorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetSchedulesBySupervisorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test1- Test_Schedules_And_Lines
            generator.Test_SupervisorInfo_Scenery_OK_Test_Schedules_And_Lines();

                //Test1.1 - Não tem sechedule no dia atual
                var responseTest1_1 = controller.SupervisorInfo("rfirebase", null).GetAwaiter().GetResult();
                Assert.IsType<OkObjectResult>(responseTest1_1);
                var rsiTest1_1 = (responseTest1_1 as OkObjectResult)?.Value as ResponseSupervisorInfo;
                Assert.NotNull(rsiTest1_1);
                Assert.Equal(3, rsiTest1_1?.Worker.Id);
                Assert.Equal(1, rsiTest1_1?.Supervisor.Id);
                Assert.Empty(rsiTest1_1?.listSWL);

                //Test1.2 - Tem sechedule no dia inserido
                var responseTest1_2 = controller.SupervisorInfo("rfirebase", new DateTime(2023, 11, 21, 0, 0, 0)).GetAwaiter().GetResult();
                Assert.IsType<OkObjectResult>(responseTest1_2);
                var rsiTest1_2 = (responseTest1_2 as OkObjectResult)?.Value as ResponseSupervisorInfo;
                Assert.NotNull(rsiTest1_2);
                Assert.Equal(3, rsiTest1_2?.Worker.Id);
                Assert.Equal(1, rsiTest1_2?.Supervisor.Id);
                Assert.Equal(1, rsiTest1_2?.listSWL.Count);
                Assert.Equal(1, rsiTest1_2?.listLine.Count);
        }

        //---------------------------------------GetProductionsInfo---------------------------------------
        /// <summary>
        /// Assegura que o método "GetProductionsInfo" retorne "NotFound" num cenário em que não consiga identificar a linha de 
        /// produção correspondente ao pedido.
        /// </summary>
        [Fact]
        public void GetProductionsInfo_ReturnsNotFound()
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
            var response = controller.GetProductionsInfo(1, null, null).GetAwaiter().GetResult();


            //Test1 - Line_NotFound
            Assert.IsType<NotFoundObjectResult>(response);
            var rgpi = (response as NotFoundObjectResult)?.Value as ResponseGetProductionsInfo;
            Assert.NotNull(rgpi);
            Assert.Equal("Erro ao identificar a Line!!", rgpi?.Message);
        }

        /// <summary>
        /// Assegura que o método "SupervisorInfo" retorna "OK" em vários cenários válidos, incluindo testes com diferentes
        /// parâmetros para verificar o retorno de informações, como produções ocorridas.
        /// </summary>
        [Fact]
        public void GetProductionsInfo_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha2" , Priority = true, CoordinatorId = 1},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 30, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 2, Goal = 100 ,Name = "Plano de produção 2", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 30, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 2},
             };
            generator.fakeProductions = new List<Production>
            {
               new Production { Id = 1, Hour = 8, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 2},
               new Production { Id = 2, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 2}
            };

            //trocar os dados do dataServices pelos do datagenerator 
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetProductionsByProdPlanId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductionsByProdPlanId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var response = controller.GetProductionsInfo(1, new DateTime(2023, 7, 30, 0, 0, 0), null).GetAwaiter().GetResult();

            //Teste 1 - Não existem Planos de produções nessas datas
            Assert.IsType<OkObjectResult>(response);
            var rgpi = (response as OkObjectResult)?.Value as ResponseGetProductionsInfo;
            Assert.NotNull(rgpi);
            Assert.Equal("Não existem Planos de produções nessas datas", rgpi?.Message);

            //Teste 2 - Não existem produções nessas datas
            var response2 = controller.GetProductionsInfo(1, null, null).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response2);
            var rgpi2 = (response2 as OkObjectResult)?.Value as ResponseGetProductionsInfo;
            Assert.NotNull(rgpi2);
            Assert.False(rgpi2?.listProductions.Any());
            Assert.Equal("Não foram encontradas produções nessa linha", rgpi2?.Message);

            //Teste 3 - Não existem produções nessas datas
            var response3 = controller.GetProductionsInfo(2, null, null).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response2);
            var rgpi3 = (response3 as OkObjectResult)?.Value as ResponseGetProductionsInfo;
            Assert.NotNull(rgpi3);
            Assert.True(rgpi3?.listProductions.Any());
            Assert.Equal(2, rgpi3?.listProductions.Count);
            Assert.Equal("Info obtida com sucesso!!", rgpi3?.Message);
        }


        //---------------------------------------GetComponentsDeviceInfo---------------------------------------

        /// <summary>
        /// Garante que o método "GetComponentsDeviceInfo" retorna "NotFound" quando não é possível identificar
        /// o dispositivo.
        /// </summary>
        [Fact]
        public void GetComponentsDeviceInfo_DeviceNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentProductsByProductId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentProductsByProductId(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test1 - Device_NotFound
            generator.Test_GetComponentsDeviceInfo_Scenery_Device_NotFound();

            var responseTest1 = controller.GetComponentsDeviceInfo(4).GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(responseTest1);
            var rgcdiTest1 = (responseTest1 as NotFoundObjectResult)?.Value as ResponseGetComponentsDeviceInfo;
            Assert.NotNull(rgcdiTest1);
            Assert.Equal("Erro ao identificar o Device!!", rgcdiTest1?.Message);          
        }

        /// <summary>
        /// Garante que o método "GetComponentsDeviceInfo" retorna "NotFound" quando não é possível identificar
        /// a linha de produção associada ao dispositivo.
        /// </summary>
        [Fact]
        public void GetComponentsDeviceInfo_LineNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentProductsByProductId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentProductsByProductId(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test2 - Line_NotFound
            generator.Test_GetComponentsDeviceInfo_Scenery_Line_NotFound();
            var responseTest2 = controller.GetComponentsDeviceInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest2);
            var rgcdiTest2 = (responseTest2 as NotFoundObjectResult)?.Value as ResponseGetComponentsDeviceInfo;
            Assert.NotNull(rgcdiTest2);
            Assert.Equal("Erro ao identificar a linha de produção!!", rgcdiTest2?.Message);
        }

        /// <summary>
        /// Garante que o método "GetComponentsDeviceInfo" retorna "NotFound" quando não é possível identificar um plano
        /// de produção no dia atual para a linha de produção.
        /// </summary>
        [Fact]
        public void GetComponentsDeviceInfo_ProductionPlanNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentProductsByProductId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentProductsByProductId(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test3 - ProductionPlan_NotFound
            generator.Test_GetComponentsDeviceInfo_Scenery_ProductionPlan_NotFound();
            var responseTest3 = controller.GetComponentsDeviceInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest3);
            var rgcdiTest3 = (responseTest3 as NotFoundObjectResult)?.Value as ResponseGetComponentsDeviceInfo;
            Assert.NotNull(rgcdiTest3);
            Assert.Equal("Não existe nenhum plano de produção na linha no momento!!", rgcdiTest3?.Message);
        }

        /// <summary>
        /// Garante que o método "GetComponentsDeviceInfo" retorna "NotFound" quando não é possível identificar um plano
        /// produto em produção na linha de produção associada ao dispositivo.
        /// </summary>
        [Fact]
        public void GetComponentsDeviceInfo_ProductNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentProductsByProductId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentProductsByProductId(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test4 - Product_Not_Found
            generator.Test_GetComponentsDeviceInfo_Scenery_Product_Not_Found();

            var responseTest4 = controller.GetComponentsDeviceInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest4);
            var rgcdiTest4 = (responseTest4 as NotFoundObjectResult)?.Value as ResponseGetComponentsDeviceInfo;
            Assert.NotNull(rgcdiTest4);
            Assert.Equal("Erro ao identificar a product!!", rgcdiTest4?.Message);
        }

        /// <summary>
        /// Assegura que o método "GetComponentsDeviceInfo" retorna "OK" em um cenário de dados válido e testa os retornos 
        /// para verificar se incluem os componentes do produto a ser fabricado na linha de produção à qual o dispositivo está associado.
        /// </summary>
        [Fact]
        public void GetComponentsDeviceInfo_ReturnsOK()
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
            generator.fakeLines = new List<Line>
            {
               new Line { Id = 1, Name = "Linha1" , Priority = true, CoordinatorId = 10},
               new Line { Id = 2, Name = "Linha2" , Priority = true, CoordinatorId = 10},
               new Line { Id = 3, Name = "Linha3" , Priority = true, CoordinatorId = 10},
            };
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            generator.fakeProducts = new List<Product>
                {
                   new Product { Id = 1, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),}
                };
            generator.fakeComponentProducts = new List<ComponentProduct>
            {
               new ComponentProduct { Id = 1, ProductId = 1, ComponentId = 1, Quantidade = 1},
               new ComponentProduct { Id = 2, ProductId = 1, ComponentId = 2, Quantidade = 1},
            };
            generator.fakeComponents = new List<Component>
            {
               new Component { Id = 1, Name = "Component1", Reference = "Comp1", Category = 1},
               new Component { Id = 2, Name = "Component2", Reference = "Comp2", Category = 1},
            };


            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetDeviceById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetDeviceById(id));
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));
            _dataService.Setup(x => x.GetComponentProductsByProductId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentProductsByProductId(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.GetComponentsDeviceInfo(1).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var rgcdi = (response as OkObjectResult)?.Value as ResponseGetComponentsDeviceInfo;
            Assert.NotNull(rgcdi);
            Assert.Equal("Info obtida com sucesso!!", rgcdi?.Message);
            Assert.Equal(2, rgcdi?.listComponents.Count);
            Assert.Contains(rgcdi?.listComponents, c => c.Id == 1);
            Assert.Contains(rgcdi?.listComponents, c => c.Id == 2);
        }

        //---------------------------------------ProductInfo---------------------------------------

        /// <summary>
        /// Garante que o método "ProductInfo" retorna "NotFound" quando não é 
        /// possível identificar a linha de produção
        /// </summary>
        [Fact]
        public void ProductInfo_LineNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test1 - Line_NotFound
            generator.Test_ProductInfo_Scenery_Line_NotFound();

            var responseTest1 = controller.ProductInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest1);
            var rpiTest1 = (responseTest1 as NotFoundObjectResult)?.Value as ResponseProductInfo;
            Assert.NotNull(rpiTest1);
            Assert.Equal("Erro ao identificar a Line!!", rpiTest1?.Message);
           
        }

        /// <summary>
        /// Garante que o método "ProductInfo" retorna "NotFound" quando não é 
        /// possível identificar um plano de produção para a linha de produção no dia atual.
        /// </summary>
        [Fact]
        public void ProductInfo_ProductionPlanNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test2 - Production_Plan_NotFound
            generator.Test_ProductInfo_Scenery_Production_Plan_NotFound();

            var responseTest2 = controller.ProductInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest2);
            var rpiTest2 = (responseTest2 as NotFoundObjectResult)?.Value as ResponseProductInfo;
            Assert.NotNull(rpiTest2);
            Assert.Equal("Não existe nenhum plano de produção na linha no momento!!", rpiTest2?.Message);

        }

        // <summary>
        /// Garante que o método "ProductInfo" retorna "NotFound" quando não é 
        /// possível identificar o produto.
        /// </summary>
        [Fact]
        public void ProductInfo_ProductNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Test3 - Product_NotFound
            generator.Test_ProductInfo_Scenery_Product_NotFound();

            var responseTest3 = controller.ProductInfo(1).GetAwaiter().GetResult();

            Assert.IsType<NotFoundObjectResult>(responseTest3);
            var rpiTest3 = (responseTest3 as NotFoundObjectResult)?.Value as ResponseProductInfo;
            Assert.NotNull(rpiTest3);
            Assert.Equal("Erro ao identificar a product!!", rpiTest3?.Message);

        }

        /// <summary>
        /// Assegura que o método "ProductInfo" retorna "OK" para um cenário de dados válido e testa os retornos para verificar se incluem as 
        /// informações do produto a ser fabricado na linha de produção para o cenário testado.
        /// </summary>
        [Fact]
        public void ProductInfo_ReturnsOK()
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
            generator.fakeProduction_Plans = new List<Production_Plan>
                {
                   new Production_Plan { Id = 1, Goal = 100 ,Name = "Plano de produção 1", InitialDate = new DateTime(2023, 6, 28, 0, 0, 0)
                   ,EndDate = new DateTime(2023, 6, 29, 0, 0, 0), Shift = 1, ProductId = 1, LineId = 1},
                   new Production_Plan { Id = 22, Goal = 100 ,Name = "Plano de produção 4", InitialDate = DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(1), Shift = 1, ProductId = 1, LineId = 1}
                };
            generator.fakeProducts = new List<Product>
                {
                   new Product { Id = 1, LabelReference = "prod1", Cycle = new TimeSpan(0, 0, 0),},
                   new Product { Id = 2, LabelReference = "prod2", Cycle = new TimeSpan(0, 0, 0),}
                };

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetProdPlansByLineId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProdPlansByLineId(id));
            _dataService.Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetProductById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.ProductInfo(1).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var rpi = (response as OkObjectResult)?.Value as ResponseProductInfo;
            Assert.NotNull(rpi);
            Assert.Equal("Info obtida com sucesso", rpi?.Message);
            Assert.Equal(1, rpi?.Product.Id);

        }

        //---------------------------------------CoordinatorInfo---------------------------------------

        /// <summary>
        /// Garante que o método "CoordinatorInfo" retorne "NotFound" quando nãoé possivel identificar o trabalhador.
        /// </summary>
        [Fact]
        public void CoordinatorInfo_WorkerNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetCoordinatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorByWorkerId(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste 1 - WorkerNotFound
            generator.Test_CoordinatorInfo_Scenery_Worker_NotFound();

            var responseTest1 = controller.CoordinatorInfo("dsadasads").GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(responseTest1);
            var rciTest1 = (responseTest1 as NotFoundObjectResult)?.Value as ResponseCoordinatorInfo;
            Assert.NotNull(rciTest1);
            Assert.Equal("Erro ao identificar o worker!!", rciTest1?.Message);

        }

        /// <summary>
        /// Garante que o método "CoordinatorInfo" retorne "NotFound" quando não é possivel identificar o coordenador.
        /// </summary>
        [Fact]
        public void CoordinatorInfo_CoordinatorNotFound()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetCoordinatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorByWorkerId(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste 2 - CoordinatorNotFound
            generator.Test_CoordinatorInfo_Scenery_Coordinator_NotFound();

            var responseTest2 = controller.CoordinatorInfo("rfirebase").GetAwaiter().GetResult();
            Assert.IsType<NotFoundObjectResult>(responseTest2);
            var rciTest2 = (responseTest2 as NotFoundObjectResult)?.Value as ResponseCoordinatorInfo;
            Assert.NotNull(rciTest2);
            Assert.Equal("Erro ao identificar o Coordinator!!", rciTest2?.Message);
            Assert.Equal(3, rciTest2?.Worker.Id);

        }

        /// <summary>
        /// Assegura que o método "CoordinatorInfo" retorne "OK" para vários cenários de dados válidos e verifica se os retornos 
        /// incluem as informações do coordenador e das linhas sob sua coordenação.
        /// </summary>
        [Fact]
        public void CoordinatorInfo_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetWorkerByIdFirebase(It.IsAny<string>())).ReturnsAsync((string id) => generator.GetWorkerByIdFirebase(id));
            _dataService.Setup(x => x.GetCoordinatorByWorkerId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetCoordinatorByWorkerId(id));
            _dataService.Setup(x => x.GetLinesByCoordinatorId(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLinesByCoordinatorId(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            //Teste 1 - OK_There_Is_No_Lines
            generator.Test_CoordinatorInfo_Scenery_OK_There_Is_No_Lines();

            var responseTest1 = controller.CoordinatorInfo("rfirebase").GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(responseTest1);
            var rciTest1 = (responseTest1 as OkObjectResult)?.Value as ResponseCoordinatorInfo;
            Assert.NotNull(rciTest1);
            Assert.Equal("Info obtida com sucesso!!", rciTest1?.Message);
            Assert.Equal(3, rciTest1?.Worker.Id);
            Assert.Equal(1, rciTest1?.Coordinator.Id);
            Assert.Empty(rciTest1?.listLine);
            //Teste 2 - OK_There_Is_Lines
            generator.Test_CoordinatorInfo_Scenery_OK_There_Is_Lines();

            var responseTest2 = controller.CoordinatorInfo("rfirebase").GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(responseTest2);
            var rciTest2 = (responseTest2 as OkObjectResult)?.Value as ResponseCoordinatorInfo;
            Assert.NotNull(rciTest2);
            Assert.Equal("Info obtida com sucesso!!", rciTest2?.Message);
            Assert.Equal(3, rciTest2?.Worker.Id);
            Assert.Equal(1, rciTest2?.Coordinator.Id);
            Assert.NotEmpty(rciTest2?.listLine);

        }

        //---------------------------------------NotificationRecomendation---------------------------------------

        /// <summary>
        /// Assegura que o método "NotificationRecomendation" retorne "BadRequest" quando 
        /// não é possível identificar o trabalhador
        /// </summary>
        [Fact]
        public void NotificationRecomendation_Invalid_WorkerId()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));

            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.NotificationRecommendation(1,1).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(responseTest);
            var rnr = (responseTest as BadRequestObjectResult)?.Value as ResponseNotificationRecommendation;                           
            Assert.Equal("Erro ao identificar o Worker!!", rnr?.Message);
        }

        /// <summary>
        /// Assegura que o método "NotificationRecomendation" retorne "BadRequest" quando 
        /// não é há requests de histórico suficientes para gerar uma reesposta
        /// </summary>
        [Fact]
        public void NotificationRecomendation_InsufficientRequests()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            generator.fakeWorkers.Add(new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2 });

            var requests = new List<Request>();
            requests.Add(new Request { Id = 1, Type = 1, WorkerId = 1, LineId = 1, Date = DateTime.Now });

            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));

            var fakeRequests = requests.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.requests).Returns(fakeRequests);
            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.NotificationRecommendation(1, 1).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(responseTest);
            var rnr = (responseTest as BadRequestObjectResult)?.Value as ResponseNotificationRecommendation;
            Assert.Equal("É necessário um mínimo de 3 datas para realizar a previsão", rnr?.Message);
        }

        /// <summary>
        /// Assegura que o método "NotificationRecomendation" retorne "OK" quando 
        /// é possível identificar o trabalhador e este já tenha 3 pedidos registados na base de dados
        /// </summary>
        [Fact]
        public void NotificationRecomendation_Ok()
        {
            // Arrange
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            generator.fakeWorkers.Add(new Worker { Id = 1, IdFirebase = "hafirebase", UserName = "Hugo Anes", Email = "ha@gmail.com", Role = 2 });

            var requests = new List<Request>();
            requests.Add(new Request { Id = 1, Type = 1, WorkerId = 1, LineId = 1, Date = DateTime.Now.AddDays(-15) });
            requests.Add(new Request { Id = 1, Type = 1, WorkerId = 1, LineId = 1, Date = DateTime.Now.AddDays(-30) });
            requests.Add(new Request { Id = 1, Type = 1, WorkerId = 1, LineId = 1, Date = DateTime.Now.AddDays(-45) });
            _dataService.Setup(x => x.GetWorkerById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetWorkerById(id));

            var fakeRequests = requests.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.requests).Returns(fakeRequests);
            // Act
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);
            var responseTest = controller.NotificationRecommendation(1, 1).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkObjectResult>(responseTest);
            var rnr = (responseTest as OkObjectResult)?.Value as ResponseNotificationRecommendation;
            Assert.Equal("Info obtida com sucesso", rnr?.Message);
        }

        //---------------------------------------GetMissingComponents---------------------------------------

        /// <summary>
        /// Assegura que o método "GetMissingComponents" retorne "OK" para um cenários de dados válidos e verifica
        /// se os retornos incluem as informações dos componentes em falta consoante os parametros de entrada.
        /// </summary>
        [Fact]
        public void GetMissingComponents_ReturnsOK()
        {
            // Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            generator.TestGetMissingComponents_Scenery_OK();

            //trocar os dados do _context pelos do datagenerator
            var fakeMissingComponentes = generator.GetMissingComponents().AsQueryable().BuildMock();
            A.CallTo(() => fakeContext.missingComponents.AsQueryable()).Returns(fakeMissingComponentes);

            //trocar os dados do dataServices pelos do datagenerator
            _dataService.Setup(x => x.GetLineById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetLineById(id));
            _dataService.Setup(x => x.GetComponentById(It.IsAny<int>())).ReturnsAsync((int id) => generator.GetComponentById(id));

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.GetMissingComponents(1,null,null).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response);
            var rgmc = (response as OkObjectResult)?.Value as ResponseGetMissingComponents;
            Assert.NotNull(rgmc);
            Assert.Equal("Info obtida com sucesso!!", rgmc?.Message);
            Assert.Single(rgmc?.listMissingComponentes);

            var response2 = controller.GetMissingComponents(2, null, null).GetAwaiter().GetResult();
            Assert.IsType<OkObjectResult>(response2);
            var rgmc2 = (response2 as OkObjectResult)?.Value as ResponseGetMissingComponents;
            Assert.NotNull(rgmc2);
            Assert.Equal("Info obtida com sucesso!!", rgmc2?.Message);
            Assert.Empty(rgmc2?.listMissingComponentes);
        }

        //---------------------------------------GetAlertsHistory---------------------------------------
        /// <summary>
        /// Assegura que o método "GetAlertsHistory" retorna "OK" e verifica o retorno das informações 
        /// do histórico de alertas fictício.
        /// </summary>
        [Fact]
        public void GetAlertsHistory_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            generator.fakeAlertsHistories = new List<AlertsHistory>
            {
                new AlertsHistory { Id = 1, TypeOfAlet = 1 , AlertSuccessfullySent = true, ErrorMessage = "", AlertMessage = "nova produção", AlertDate = DateTime.Now },
            };

            //trocar os dados do _context pelos do datagenerator
            var fakeAlertsHistorys = generator.fakeAlertsHistories.AsQueryable().BuildMock();
            A.CallTo(() => fakeContext.alertsHistories.AsQueryable()).Returns(fakeAlertsHistorys);

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.GetAlertsHistory(1,null,null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var list = (response as OkObjectResult)?.Value as List<AlertsHistory>;
            Assert.NotNull(list);
            Assert.Contains(list, a => a.Id == 1);

            var response2 = controller.GetAlertsHistory(0, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response2);
            var list2 = (response2 as OkObjectResult)?.Value as List<AlertsHistory>;
            Assert.Equal(0,list2?.Count);
        }


        //---------------------------------------GetAlertsHistory---------------------------------------
        /// <summary>
        /// Assegura que o método "GetRquests" retorna "OK" e verifica o retorno das informações 
        /// do histórico de requets fictício.
        /// </summary>
        [Fact]
        public void GetRquests_ReturnsOK()
        {
            //Inicializar as mocks
            var systemLogic = new SystemLogic();
            var _dataService = new Mock<IDataService>();
            var generator = new DataGenerator();
            var fakeContext = A.Fake<IContextAwareDb>();

            //Atribuir valores ao dados que vou utilizar
            var fakeRequestsHistories = new List<Request>
            {
                new Request { Id = 1, Type = 1 , WorkerId = 1, LineId = 1, Date = DateTime.Now },
            };

            //trocar os dados do _context pelos do datagenerator
            var fakeRequests = fakeRequestsHistories.AsQueryable().BuildMock();
            A.CallTo(() => fakeContext.requests.AsQueryable()).Returns(fakeRequests);

            //Inicializar Controllador com as Mocks
            var controller = new ContextServerController(fakeContext, systemLogic, _dataService.Object);

            var response = controller.GetRequestsHistory(1, null, null).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response);
            var list = (response as OkObjectResult)?.Value as List<Request>;
            Assert.NotNull(list);
            Assert.Contains(list, a => a.Id == 1);

            var response2 = controller.GetRequestsHistory(1, null, 2).GetAwaiter().GetResult();

            Assert.IsType<OkObjectResult>(response2);
            var list2 = (response2 as OkObjectResult)?.Value as List<Request>;
            Assert.Equal(0, list2?.Count);
        }


    }
}

