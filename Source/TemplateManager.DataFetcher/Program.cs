using System;
using System.IO;
using TemplateManager.DataFetcher.Abstractions;
using TemplateManager.DataFetcher.CacheManagers;
using TemplateManager.DataFetcher.DataProviders;
using TemplateManager.DataFetcher.DataTargets;
using TemplateManager.DataFetcher.Logging;
using TemplateManager.DataFetcher.Parsers;
using TemplateManager.DataFetcher.Services;

namespace TemplateManager.DataFetcher
{
    internal class Program
    {
        private static void Main()
        {
            Console.SetBufferSize(130, 9999);
            Console.SetWindowSize(130, 60);

            var cacheFile = Path.GetFullPath("cache.bin");
            var imageStore = Path.GetFullPath("images");

            const string domain = "wiki.guildwars.com";

            if(!Directory.Exists(imageStore))
                Directory.CreateDirectory(imageStore);

            var logger = CreateLogger();
            var fileSystem = new FileSystemAbstraction();
            var cacheManager = new DatabaseCacheManager();
            var service = CreateService(domain, imageStore, logger, cacheManager, fileSystem);

            service.Execute();

            cacheManager.TearDown();
            logger.TearDown();
        }

        private static IService CreateService(string domain,
                                              string imageStore,
                                              ILogger logger,
                                              ICacheManager cacheManager,
                                              IFileSystemAbstraction fileSystem)
        {
            var dataProvider = new DataProvider(logger, domain, cacheManager, new NetworkAbstraction(logger));

            var translationProvider = new TranslationProvider(dataProvider, logger);
            var parser = new Parser(logger, dataProvider, fileSystem, imageStore);

            var targets = new IDataTarget[]
                              {
                                  new TemplateManagerTarget(logger),
                                  new TextFileTarget(logger, "skills.txt")
                              };
            return new Service(logger, parser, targets, translationProvider, 1, 3500);
        }

        private static ILogger CreateLogger()
        {
            var logFormatter = new TimestampFormatter();
            return new MultiLogger(new ILogger[]
                                       {
                                           new ConsoleLogger(logFormatter, LogSensitivity.InformationHigh),
                                           new HTMLLogger(logFormatter,
                                                          string.Format("{0:yyyy-MM-dd HHmm}.html", DateTime.Now))
                                       });
        }
    }
}