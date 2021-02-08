using System;
using System.Threading.Tasks;
using bc.BlockManagers;
using Microsoft.Extensions.Configuration;

namespace bc
{
    internal class App
    {
        private readonly IConfiguration _config;
        private readonly IManageBlockChain _blockChainManager;

        public App(IConfiguration config, IManageBlockChain blockManager)
        {
            _config = config;
            _blockChainManager = blockManager;
        }

        public async Task RunAsync()
        {
            var logDirectory = _config.GetValue<string>("Runtime:LogOutputDirectory");

            bool interactionComplete = false;
            bool stopPressed = false;
            var threads = 1;

            Console.WriteLine($"Welcome to FC Coin");
            Console.WriteLine($"Enter ID: ");

            while (!interactionComplete)
            {
                Console.WriteLine($"Choose Mining Option: ");
                Console.WriteLine($"1 - Multithreaded, 2 - Single Thread");

                int.TryParse(Console.ReadLine(), out int miningOption);

                if (miningOption < 1 || miningOption > 2)
                {
                    Console.WriteLine("Invalid Option, try again.");
                    continue;
                }
                else if (miningOption == 1)
                {
                    Console.Write("Threads to use: ");
                    int.TryParse(Console.ReadLine(), out threads);

                    if (threads > 0)
                    {
                        interactionComplete = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Option, try again.");
                    }
                }
                else
                {
                    interactionComplete = true;
                }
            }

            if(threads > 1)
            {
                while (!stopPressed)
                {
                    await _blockChainManager.ManageAsync(threads);
                }
            }
            else
            {
                while (!stopPressed)
                {
                    await _blockChainManager.Manage();
                }
            }
            
        }
    }
}