using Moq;
using NUnit.Framework;
using YaIoCContainer.Tests.Abstractions;
using YaIoCContainer.Tests.Stubs;

namespace YaIoCContainer.Tests
{
    [Category("YaIoCContainer")]
    [TestFixture]
    public class YaIoCContainerTest
    {
        private YaIoCContainer _container;

        [SetUp]
        public void  Setup()
        {
            _container = new YaIoCContainer();
        }

        [Test]
        public void Resolve_ForRegisteredType_ReturnsExpectedType()
        {
            //Arrange
            _container.RegisterType<IInjectedInterface, InjectedImplementationStub>();
            _container.RegisterType<IYaInjectedInterface, YaImplementationStub>();
            _container.RegisterType<ConsumerStub, ConsumerStub>();
            //Act
            var actualResult = _container.Resolve<ConsumerStub>();
            //Assert
            Assert.IsInstanceOf<ConsumerStub>(actualResult);
        }

        [Test]
        public void Resolve_ForRegisteredInstance_ReturnsExpectedType()
        {
            //Arrange
            var injectedTypeMock = new Mock<IInjectedInterface>();
            _container.RegisterInstanceOf<IInjectedInterface>(injectedTypeMock.Object);
            var yaInjectedTypeMock = new Mock<IYaInjectedInterface>();
            _container.RegisterInstanceOf<IYaInjectedInterface>(yaInjectedTypeMock.Object);
            _container.RegisterType<ConsumerStub, ConsumerStub>();
            //Act
            var actualResult = _container.Resolve<ConsumerStub>();
            //Assert
            Assert.IsInstanceOf<ConsumerStub>(actualResult);
        }
    }
}
