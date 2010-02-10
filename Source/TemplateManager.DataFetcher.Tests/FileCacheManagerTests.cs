using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.CacheManagers;
using TemplateManager.DataFetcher.Logging;

namespace TemplateManager.DataFetcher.Tests
{
    [TestClass]
    public class FileCacheManagerTests
    {
        private IFileSystemAbstraction fileSystem;
        private ILogger logger;

        private Mock<IFileSystemAbstraction> mockedFileSystem;
        private Mock<ILogger> mockedLogger;
        private ICacheManager target;

        [TestInitialize]
        public void Initialize()
        {
            mockedLogger = new Mock<ILogger>();
            mockedLogger.Setup(o => o.Log(It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LogSeverity>()));

            mockedFileSystem = new Mock<IFileSystemAbstraction>();

            logger = mockedLogger.Object;
            fileSystem = mockedFileSystem.Object;

            target = new FileCacheManager(logger, fileSystem, "cache.bin");
        }

        [TestMethod]
        public void VirtualizedLoadTest()
        {
            mockedFileSystem.Setup(o => o.Exists("cache.bin")).Returns(true);

            var reader = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(reader, new Dictionary<string, string>());
            reader.Seek(0, SeekOrigin.Begin);

            mockedFileSystem.Setup(o => o.Open("cache.bin", FileMode.Open, FileAccess.Read, FileShare.Read)).Returns(
                reader).Verifiable();

            var writer = new MemoryStream();
            mockedFileSystem.Setup(o => o.Open("cache.bin", FileMode.Create, FileAccess.Write, FileShare.None)).Returns(
                writer).Verifiable();

            var result = target.Load("test");

            target.TearDown();

            Assert.IsNull(result);
            mockedFileSystem.Verify();
        }

        [TestMethod]
        public void VirtualizedLoadTest2()
        {
            mockedFileSystem.Setup(o => o.Exists("cache.bin")).Returns(true);

            var reader = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(reader, new Dictionary<string, string> {{"test", "this is a test"}});
            reader.Seek(0, SeekOrigin.Begin);

            mockedFileSystem.Setup(o => o.Open("cache.bin", FileMode.Open, FileAccess.Read, FileShare.Read)).Returns(
                reader).Verifiable();

            var writer = new MemoryStream();
            mockedFileSystem.Setup(o => o.Open("cache.bin", FileMode.Create, FileAccess.Write, FileShare.None)).Returns(
                writer).Verifiable();

            var result = target.Load("test");

            target.TearDown();

            Assert.AreEqual("this is a test", result);
            mockedFileSystem.Verify();
        }

        [TestMethod]
        [Ignore]
        public void LiveLoadTest()
        {
            var cacheManager = new FileCacheManager(logger, new FileSystemAbstraction(), "cache.bin");
            var result = cacheManager.Load("test");
            cacheManager.TearDown();
        }
    }
}