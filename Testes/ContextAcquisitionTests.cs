using ContextAcquisition.Data;
using ContextAcquisition.Services;
using ContinentalTestDb.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Models.ContextModels;
using Models.FunctionModels;
using Services.DataServices;

namespace Testes
{

    public class ContextAcquisitionTests
    {
        [Fact]
        public void TestSendAlert()
        {
            // Arrange
            var fakeContext = A.Fake<ContextAcquisitonDb>();
            var fakeDataService = A.Fake<IDataService>();


            var sendAlertRequest = new SendAlertRequest
            {
                Production = new Production { Id = 1, Hour = 10, Day = new DateTime(2023, 6, 29, 0, 0, 0), Quantity = 10, Production_PlanId = 1 },
                Message = $"Mensagem de teste",
            };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var logic = new Logic(fakeContext, fakeDataService);
            logic.SendAlert(sendAlertRequest, 2).GetAwaiter().GetResult();


            // Assert
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedErrorMessage = "Erro ao enviar alerta de Produção";
            Assert.Contains(expectedErrorMessage, consoleOutput.ToString());
        }

    }
}