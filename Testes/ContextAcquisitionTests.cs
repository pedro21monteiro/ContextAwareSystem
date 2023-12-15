using ContextAcquisition.Data;
using ContextAcquisition.Services;
using ContinentalTestDb.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using MockQueryable.FakeItEasy;
using Models.ContextModels;
using Models.FunctionModels;
using Services.DataServices;
using System.Net;

namespace Tests
{
    public class ContextAcquisitionTests
    {
        //---------------------- Testes � funcionalidade SendAlert ----------------------


        /// <summary>
        /// Verifica se a fun��o executa corretamente quando o envio de alerta � bem-sucedido.
        /// </summary>       
        [Fact]
        public void SendAlert_Successful()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            var sendAlertRequest = new SendAlertRequest
            {
                Production = new Production {Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), 
                    Quantity = 10, Production_PlanId = 1 },
                Message = $"Mensagem de teste",
            };

            // Para conseguir verificar informa��es na consola.
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.SendAlert(sendAlertRequest, 2).GetAwaiter().GetResult();

            // Assert
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Alerta da Produ��o: ID-{sendAlertRequest.Production.Id} Enviado com sucesso.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifica se a fun��o executa corretamente quando o envio de alerta n�o � bem-sucedido.
        /// </summary>
        [Fact]
        public void SendAlert_Failure()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            var sendAlertRequest = new SendAlertRequest
            {
                Stop = new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0) },
                Message = $"Mensagem de teste",
            };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.SendAlert(sendAlertRequest, 1).GetAwaiter().GetResult();

            // Assert
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Erro ao enviar alerta de Paragem: ID-{sendAlertRequest.Stop.Id}.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }


        /// <summary>
        /// Garante que um alerta � enviado quando a produ��o atende aos crit�rios para envio.
        /// </summary>
        [Fact]
        public void CheckIfItIsNewProduction_SentAlert()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
           .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            //inicializar uma produ��o nas ultimas 24 horas
            var production = new Production { Id = 1, Hour = DateTime.Now.Hour, Day = DateTime.Now.Date, Quantity = 10, Production_PlanId = 1 };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.CheckIfItIsNewProduction(production).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        }

        /// <summary>
        /// Garante que nenhum alerta � enviado quando a produ��o n�o atende aos crit�rios para envio.
        /// </summary>
        [Fact]
        public void CheckIfItIsNewProduction_NoAlertSent()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
           .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            //inicializar uma produ��o que n�o � das ultimas 24 horas por isso n�o pode enviar alerta
            var production = new Production { Id = 1, Hour = DateTime.Now.Hour, Day = DateTime.Now.Date.AddDays(-2), Quantity = 10, Production_PlanId = 1 };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.CheckIfItIsNewProduction(production).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert n�o foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustNotHaveHappened();

            var expectedMessage = $"Production: ID-{production.Id} n�o � das ultimas 24 horas, n�o enviar notifica��o.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }


        /// <summary>
        /// Garante que um alerta � enviado quando uma paragem atende aos crit�rios para envio.
        /// </summary>
        [Fact]
        public void CheckIfIsUrgentStop_SentAlert()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
           .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            //paragem considerada urgente, � n�o planeada e ocorreu no dia atual e durou mais de 15 min
            var stop = new Stop { Id = 1, Planned = false, Duration = new TimeSpan(0,16,0) ,InitialDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(15) };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.CheckIfIsUrgentStop(stop).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        /// <summary>
        /// Garante que um alerta � enviado quando uma n�o atende aos crit�rios para envio.
        /// </summary>
        [Fact]
        public void CheckIfIsUrgentStop_NoAlertSent()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
           .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            //paragem considerada n�o urgente, � n�o planeada e ocorreu no dia atual mas n�o durou mais de 15 min
            var stop = new Stop { Id = 1, Planned = false, Duration = new TimeSpan(0, 14, 0), InitialDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(15) };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.CheckIfIsUrgentStop(stop).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert n�o foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustNotHaveHappened();

            var expectedMessage = $"Stop: ID-{stop.Id} n�o � urgente.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifica se a fun��o adiciona corretamente uma nova produ��o quando n�o h� uma produ��o existente com o mesmo ID.
        /// </summary>
        [Fact]
        public void UpdateProduction_AddsNewProduction()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var production = new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1 };

            //a lista est� vazia para ao procurar o elemento production nessa lista n�o o encontr� e vai recorrer � adic��o
            var fakeListDatabaseProductions = new List<Production>().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.Productions).Returns(fakeListDatabaseProductions);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.UpdateProduction(production).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Add(A<Production>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Production: ID-{production.Id} Adicionado com suceso";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifica se a fun��o atualiza corretamente uma produ��o existente.
        /// </summary>
        [Fact]
        public void UpdateProduction_UpdatesExistingProduction()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var production = new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1 };
            var production2 = new Production { Id = 1, Hour = 12, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1 };
            //a lista est� vazia para ao procurar o elemento production nessa lista n�o o encontr� e vai recorrer � adic��o

            var listProductions = new List<Production>();
            listProductions.Add(production2);
            var fakeListDatabaseProductions = listProductions.AsQueryable().BuildMockDbSet();    

            A.CallTo(() => fakeContext.Productions).Returns(fakeListDatabaseProductions);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.UpdateProduction(production).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Update(A<Production>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Production: ID-{production.Id} Atualizada com suceso.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifica se a fun��o adiciona corretamente uma nova paragem quando n�o h� uma paragem existente com o mesmo ID.
        /// </summary>
        [Fact]
        public void UpdateStop_AddsNewStop()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var stop = new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0) };

            //a lista est� vazia para ao procurar o elemento production nessa lista n�o o encontr� e vai recorrer � adic��o
            var fakeListDatabaseStops = new List<Stop>().AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.Stops).Returns(fakeListDatabaseStops);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.UpdateStop(stop).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Add(A<Stop>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Stop: ID-{stop.Id} Adicionado com suceso.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifica se a fun��o atualiza corretamente uma paragem existente.
        /// </summary>
        [Fact]
        public void UpdateStop_UpdatesExistingStop()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var stop = new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 28, 10, 0, 0), EndDate = new DateTime(2023, 6, 28, 10, 15, 0) };
            var stop2 = new Stop { Id = 1, Planned = false, InitialDate = new DateTime(2023, 6, 20, 10, 0, 0), EndDate = new DateTime(2023, 6, 20, 10, 15, 0) };
            //a lista est� vazia para ao procurar o elemento production nessa lista n�o o encontr� e vai recorrer � adic��o

            var listStops = new List<Stop>();
            listStops.Add(stop2);
            var fakeListDatabaseStops = listStops.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeContext.Stops).Returns(fakeListDatabaseStops);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.UpdateStop(stop).GetAwaiter().GetResult();

            // Assert
            //Para verificar se o send SendAlert foi chamado vou verificar se houve chamadas � base de dados.
            A.CallTo(() => fakeContext.Update(A<Stop>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Stop: ID-{stop.Id} Atualizado com suceso.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }
    }
}