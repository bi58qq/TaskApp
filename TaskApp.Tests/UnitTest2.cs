using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskApp.Tests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log_ShouldStoreMessagesInMockLogger()
        {
            // Arrange
            var logger = new MockLoggerService();

            // Act
            logger.Log("Hello");
            logger.Log("World");

            // Assert
            Assert.AreEqual(2, logger.Logs.Count);
            Assert.AreEqual("Hello", logger.Logs[0]);
            Assert.AreEqual("World", logger.Logs[1]);
        }
    }
}