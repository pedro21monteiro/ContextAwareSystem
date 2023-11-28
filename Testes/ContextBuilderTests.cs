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

namespace Testes
{
    public class ContextBuilderTests
    {
        //----------------------CreateRequest
        [Fact]
        public void CreateResquest_ValidRequest_ReturnsOk()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();

            var request = new Request
            {
                Type = 2,
                Date = DateTime.Now,
                WorkerId = 1,
                LineId = 1
            };
            var listRequests = new List<Request>();

            // Act
            var controller = new ContextBuilderController(fakeContext);
            var response = controller.CreateResquest(request).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            // Verifique se o método Add foi chamado para a entidade Request
            A.CallTo(() => fakeContext.Add(request)).MustHaveHappenedOnceExactly();
            // Verifique se o método SaveChangesAsync foi chamado
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        }

        //----------------------AddMissingComponent
        [Fact]
        public void AddMissingComponent_ReturnsBadRequest()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();

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
            var controller = new ContextBuilderController(fakeContext);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void AddMissingComponent_Returns_Ok()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();

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
            var controller = new ContextBuilderController(fakeContext);
            var response = controller.AddMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            // Verifique se o método Add foi chamado para a entidade MissingComponent
            A.CallTo(() => fakeContext.Add(missingComponent)).MustHaveHappenedOnceExactly();

            // Verifique se o método SaveChangesAsync foi chamado- vai ser chamado 2 vez uma para guardao o mc e outra no SendAlert
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedTwiceExactly();

        }

        //------------------RemoveMissingComponent
        [Fact]
        public void RemoveMissingComponent_Returns_Ok()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();

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
            var controller = new ContextBuilderController(fakeContext);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<OkResult>(response);

            // Verifique se o método Add foi chamado para a entidade MissingComponent
            A.CallTo(() => fakeContext.missingComponents.Remove(A<MissingComponent>.Ignored)).MustHaveHappenedOnceExactly();

            // Verifique se o método SaveChangesAsync foi chamado- vai ser chamado 2 vez uma para guardao o mc e outra no SendAlert
            A.CallTo(() => fakeContext.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public void RemoveMissingComponent_Returns_BadRequest()
        {
            // Arrange
            var fakeContext = A.Fake<IContextBuilderDb>();

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
            var controller = new ContextBuilderController(fakeContext);
            var response = controller.RemoveMissingComponent(missingComponent).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestResult>(response);
        }




    }
}