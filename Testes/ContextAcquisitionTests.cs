using ContextAcquisition.Data;
using ContextAcquisition.Services;
using ContinentalTestDb.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Models.FunctionModels;
using Services.DataServices;
using System.Net;

namespace Testes
{
    public class ContextAcquisitionTests
    {

        [Fact]
        public void TestSendAlert()
        {
            // Arrange
            var fakeContext = A.Fake<IContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

            var sendAlertRequest = new SendAlertRequest
            {
                Production = new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1 },
                Message = $"Mensagem de teste",
            };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService, fakeHttpClientWrapper);
            logic.SendAlert(sendAlertRequest, 2).GetAwaiter().GetResult();


            // Assert
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedErrorMessage = "Erro ao enviar alerta de Produção";
            Assert.Contains(expectedErrorMessage, consoleOutput.ToString());


            // Segunda chamada
            logic.SendAlert(sendAlertRequest, 2).GetAwaiter().GetResult();

            // Assert para a segunda chamada
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedTwiceExactly(); // Duas chamadas no total
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedTwiceExactly(); // Duas chamadas no total
        }

    }
}