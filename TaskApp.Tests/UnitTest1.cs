using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskApp.Services;
using System.Collections.Generic;
using System.IO;

namespace TaskApp.Tests
{
    [TestClass]
    public class JsonStorageServiceTests
    {
        [TestMethod]
        public void Load_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            var storage = new JsonStorageService();
            string path = "data.json";

            // Act
            List<string> result = storage.Load<string>(path);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void SaveAndLoad_ShouldReturnSameData()
        {
            // Arrange
            var storage = new JsonStorageService();
            string path = "test_data.json";
            var original = new List<string> { "One", "Two" };

            // Act
            storage.Save(path, original);
            var loaded = storage.Load<string>(path);

            // Assert
            Assert.AreEqual(2, loaded.Count);
            Assert.AreEqual("One", loaded[0]);
            Assert.AreEqual("Two", loaded[1]);

            // Cleanup
            if (File.Exists(path))
                File.Delete(path);
        }

    }
}