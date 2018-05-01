using CommandDecorator;
using Moq;
using NUnit.Framework;
using System;

namespace CommandDecoratorTest
{
    [Category("CommandDecorator")]
    [TestFixture]
    public class RetryOnFailCommandTest
    {
        private ICommand _retryOnFailCommand;
        private Mock<ICommand> _failCommandMock;
        private const int MaxAttempts = 3;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _failCommandMock = new Mock<ICommand>();
            _retryOnFailCommand = new RetryOnFailCommand(_failCommandMock.Object, MaxAttempts);
        }

        [Test]
        public void FailCommand_IsCalled_MaxAttemptTimes()
        {
            //Arrange
            _failCommandMock.Setup(foo => foo.Execute()).Throws(new Exception());
            //Act
            try
            {
                _retryOnFailCommand.Execute();
            }
            catch{}
            //Assert
            _failCommandMock.Verify(x => x.Execute(), Times.Exactly(MaxAttempts));
        }

        [Test]
        public void RetryOnFailCommand_ThrowsExpectedException()
        {
            //Arrange
            _failCommandMock.Setup(foo => foo.Execute()).Throws(new Exception());
            //Act
            //Assert
            Assert.Throws<Exception>(_retryOnFailCommand.Execute);
        }
    }
}
