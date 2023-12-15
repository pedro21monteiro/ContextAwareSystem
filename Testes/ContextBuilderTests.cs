using ContextBuilder;
using ContextBuilder.Controllers;
using ContextBuilder.Data;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.FakeItEasy;
using Models.ContextModels;
using Models.FunctionModels;
using Moq;
using Services.DataServices;
using System.Net;

namespace Tests
{
    public class ContextBuilderTests
    {
        //----------------------CreateResquest

        /// <summary>
        ///  Garante que o método “CreateResquest” retorne “OK” para um exemplo de dados válido
        /// </summary>
        [Fact]
        public void CreateResquest_ReturnsOk()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var request = new Request
            {
                Type = 2,
                Date = DateTime.Now,
                WorkerId = 1,
                LineId = 1
            };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.CreateResquest(request).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            A.CallTo(() => fakeContext.Add(A<Request>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Request: {request.Id} - Adicionado com Sucesso";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        ///  Garante que o método “CreateResquest” retorne “BadRequest” quando retorna uma excepção na base de dados e 
        ///  não permite adicionar o request
        /// </summary>
        [Fact]
        public void CreateResquest_ReturnsBadRequestOnException()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeContext.Add(A<Request>.Ignored))
            .Throws(new DbUpdateException("Erro ao adicionar!"));


            var request = new Request
            {
                Type = 2,
                Date = DateTime.Now,
                WorkerId = 1,
                LineId = 1
            };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.CreateResquest(request).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

            //deve ser chamado um vez que retorna uma excepção
            A.CallTo(() => fakeContext.Add(A<Request>.Ignored)).MustHaveHappenedOnceExactly();
            //Com a excepção interrompe e função e já não chama esta parte
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustNotHaveHappened();

            var expectedMessage = $"Request: {request.Id} - Erro ao adicionar";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        //----------------------AddMissingComponent

        /// <summary>
        /// Verifique se a função retorna um status Ok quando um MissingComponent é adicionado com sucesso.
        /// </summary>
        [Fact]
        public void AddMissingComponent_ReturnsOkOnSuccess()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = 2,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            var listMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            A.CallTo(() => fakeContext.Add(A<MissingComponent>.Ignored)).MustHaveHappenedOnceExactly();
            //tem de acontecer pelo menos 1x pq depois vai fazer tbm nos alertas
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceOrMore();

            var expectedMessage = $"adicionado com sucesso.";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifique se a função retorna BadRequest quando um MissingComponent com a mesma combinação de LineId e
        /// ComponentId já existe no contexto da base de dados.
        /// </summary>
        [Fact]
        public void AddMissingComponent_ReturnsBadRequestOnDuplicateMissingComponent()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = 1,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            var listMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

            A.CallTo(() => fakeContext.Add(A<MissingComponent>.Ignored)).MustNotHaveHappened();
            //tem de acontecer pelo menos 1x pq depois vai fazer tbm nos alertas
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceOrMore();

            var expectedMessage = $"O missingComponente já existe:";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Garante que a função retorna BadRequest quando LineId ou ComponentId são menores ou iguais a zero, 
        /// ou seja dados inválidos.
        /// </summary>
        [Fact]
        public void AddMissingComponent_ReturnsBadRequestOnInvalidParameters()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = -1,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            var badRequestResult = (response as BadRequestObjectResult)?.Value as string;
         

            var expectedMessage = $"Parâmetros do MissingComponente não são válidos.";
            Assert.Contains(expectedMessage, badRequestResult);
        }

        /// <summary>
        /// Garanta que a função retorna BadRequest quando ocorre alguma execeção.
        /// </summary>
        [Fact]
        public void AddMissingComponent_ReturnsBadRequestOnException()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = 2,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            var listMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            A.CallTo(() => fakeContext.Add(A<MissingComponent>.Ignored))
           .Throws(new DbUpdateException("Erro ao adicionar!"));

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
            var badRequestResult = (response as BadRequestObjectResult)?.Value as string;


            var expectedResultMessage = $"Erro ao adicionar MissingComponente.";
            Assert.Contains(expectedResultMessage, badRequestResult);

            var expectedConsoleMessage = $"Erro ao adicionar missingComponente:";
            Assert.Contains(expectedConsoleMessage, consoleOutput.ToString());
        }

        //------------------RemoveMissingComponent

        /// <summary>
        ///  Verifique se a função retorna um status Ok quando um MissingComponent é removido com sucesso.
        /// </summary>
        [Fact]
        public void RemoveMissingComponent_ReturnsOkOnSuccess()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = 1,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            var listMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            A.CallTo(() => fakeContext.missingComponents.Remove(A<MissingComponent>.Ignored)).MustHaveHappenedOnceExactly();
            //tem de acontecer pelo menos 1x pq depois vai fazer tbm nos alertas
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceOrMore();

            var expectedMessage = $"removido com sucesso";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Garante que a função retorna BadRequest quando tenta remover um MissingComponent que não existe no contexto da base de dados.
        /// </summary>
        [Fact]
        public void RemoveMissingComponent_ReturnsBadRequestOnMissingComponentNotFound()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = 2,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            var listMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

            A.CallTo(() => fakeContext.missingComponents.Remove(A<MissingComponent>.Ignored)).MustNotHaveHappened();
            //tem de acontecer pelo menos 1x pq depois vai fazer tbm nos alertas
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustNotHaveHappened();

            var expectedMessage = $"Não foi possível encontrar o missingComponente";
            Assert.Contains(expectedMessage, consoleOutput.ToString());

            var badRequestResult = (response as BadRequestObjectResult)?.Value as string;

            var expectedResultMessage = $"MissingComponente não encontrado.";
            Assert.Contains(expectedResultMessage, badRequestResult);
        }

        /// <summary>
        /// Garanta que a função retorna BadRequest quando ocorre uma exceção ao tentar remover o MissingComponent.
        /// </summary>
        [Fact]
        public void RemoveMissingComponent_ReturnsBadRequestOnException()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            var missingComponent = new MissingComponent
            {
                LineId = 1,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            var listMissingComponents = new List<MissingComponent>
                {
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 1, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            A.CallTo(() => fakeContext.missingComponents.Remove(A<MissingComponent>.Ignored))
           .Throws(new DbUpdateException("Erro ao adicionar!"));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);

            A.CallTo(() => fakeContext.missingComponents.Remove(A<MissingComponent>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustNotHaveHappened();

            var expectedMessage = $"Erro ao remover missingComponente:";
            Assert.Contains(expectedMessage, consoleOutput.ToString());

            var badRequestResult = (response as BadRequestObjectResult)?.Value as string;

            var expectedResultMessage = $"Erro ao remover MissingComponente.";
            Assert.Contains(expectedResultMessage, badRequestResult);
        }


        //------------------Send Alert
        /// <summary>
        /// Verifica se a função executa corretamente quando o envio de alerta é bem-sucedido.
        /// </summary>       
        [Fact]
        public void SendAlert_Successful()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

            var missingComponent = new MissingComponent
            {
                LineId = 1,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            // Para conseguir verificar informações na consola.
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            controller.SendAlert(missingComponent).GetAwaiter().GetResult();

            // Assert
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Alerta de componente em falta: ComponenteId - {missingComponent.ComponentId}, LineId - {missingComponent.LineId} enviado com sucesso";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }

        /// <summary>
        /// Verifica se a função executa corretamente quando o envio de alerta não é bem-sucedido.
        /// </summary>
        [Fact]
        public void SendAlert_Failure()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();
            var fakeHttpClientWrapper = A.Fake<IHttpClientWrapper>();

            A.CallTo(() => fakeHttpClientWrapper.PostAsJsonAsync(A<string>.Ignored, A<object>.Ignored))
            .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)));

            var missingComponent = new MissingComponent
            {
                LineId = 1,
                ComponentId = 1,
                OrderDate = DateTime.Now
            };

            // Para conseguir verificar informações na consola.
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            controller.SendAlert(missingComponent).GetAwaiter().GetResult();

            // Assert
            A.CallTo(() => fakeContext.Add(A<AlertsHistory>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

            var expectedMessage = $"Erro ao enviar alerta: ComponenteId - {missingComponent.ComponentId}, LineId - {missingComponent.LineId}";
            Assert.Contains(expectedMessage, consoleOutput.ToString());
        }




    }
}