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

namespace Tests
{
    public class ContextBuilderTests
    {

        //----------------------CreateRequest
       

        /// <summary>
        ///  Garante que o método “CreateResquest” retorne “OK” para um exemplo de dados válido
        /// </summary>
        [Fact]
        public void CreateResquest_ValidRequest_ReturnsOk()
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
            var listRequests = new List<Request>();

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.CreateResquest(request).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            // Verifique se o método Add foi chamado para a entidade Request
            A.CallTo(() => fakeContext.Add(request)).MustHaveHappenedOnceExactly();
            // Verifique se o método SaveChangesAsync foi chamado
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        //----------------------AddMissingComponent

        /// <summary>
        /// Garante que o método “AddMissingComponent” retorne “BadRequest” ao tentar adicionar um 
        /// componente em falta que já existe na base de dados.
        /// </summary>
        [Fact]
        public void AddMissingComponent_ReturnsBadRequest()
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

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }


        /// <summary>
        /// Garante que o método “AddMissingComponent” retorne “OK” ao tentar adicionar um componente em falta válido.
        /// </summary>
        [Fact]
        public void AddMissingComponent_ReturnsOk()
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
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 2, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            // Verifique se o método Add foi chamado para a entidade MissingComponent
            A.CallTo(() => fakeContext.Add(missingComponent)).MustHaveHappenedOnceExactly();

            // Verifique se o método SaveChangesAsync foi chamado- vai ser chamado 2 vez uma para guardao o mc e outra no SendAlert
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedTwiceExactly();

        }

        //------------------RemoveMissingComponent

        /// <summary>
        /// Garante que o método “RemoveMissingComponent” retorne “BadRequest” ao tentar remover um componente em 
        /// falta que não exista na base de dados.
        /// </summary>
        [Fact]
        public void RemoveMissingComponent_ReturnsOk()
        {
            // Preparação
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
            // Ação
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();
            // Verificação
            Assert.IsType<OkResult>(response);
            A.CallTo(() => fakeContext.missingComponents.Remove(A<MissingComponent>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        /// <summary>
        /// Garante que o método “RemoveMissingComponent” retorne “OK” ao remover um componente em falta que exista na base de dados.
        /// </summary>
        [Fact]
        public void RemoveMissingComponent_ReturnsBadRequest()
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
                   new MissingComponent { Id = 1, LineId = 1 ,ComponentId = 2, OrderDate = DateTime.Now.AddDays(-1)}
                };

            var fakeMissingComponentes = listMissingComponents.AsQueryable().BuildMockDbSet();
            A.CallTo(() => fakeContext.missingComponents).Returns(fakeMissingComponentes);

            // Act
            var controller = new ContextBuilderController(fakeContext, fakeHttpClientWrapper);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }


        

    }
}