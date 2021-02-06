using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using bc.BlockManagers;
using bc.Miners;
using bc.Retievers;
using Microsoft.Extensions.Configuration;
using bc.HashingMethods;

namespace bc
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<App>().RunAsync();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config)
                    .AddLogging(config => config.AddConsole().SetMinimumLevel(LogLevel.Debug))
                    .AddSingleton<IManageBlockChain, BlockChainManager>()
                    .AddSingleton<IGenerateNewBlocks, NewBlockGenerator>()
                    .AddSingleton<IMineBlocks, BlockMiner>()
                    .AddTransient<IRetrieveData, DataRetriever>()
                    .AddSingleton<IRetrieveBlockChain, BlockChainRetriever>()
                    .AddSingleton<IHash, HashSha256>();

            services.AddTransient<App>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}