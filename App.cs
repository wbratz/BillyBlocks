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
            int miningOption;
            var threads = 1;
            
            System.Console.WriteLine($"Welcome to FC Coin");
            System.Console.WriteLine($"Enter ID: ");

            while(!interactionComplete)
            {
                System.Console.WriteLine($"Choose Mining Option: ");
                System.Console.WriteLine($"1 - Multithreaded, 2 - Single Thread");

                int.TryParse(Console.ReadLine(), out miningOption);

                if(miningOption < 1 || miningOption > 2)
                {
                    System.Console.WriteLine("Invalid Option, try again.");
                    continue;
                }
                else if (miningOption == 2)
                {
                    System.Console.WriteLine("Threads to use: ");
                    int.TryParse(Console.ReadLine(), out threads);
                    
                    if(threads > 0)
                    {
                        interactionComplete = true;
                    }
                    else 
                    {
                       System.Console.WriteLine("Invalid Option, try again.");
                    }
                }
            }            

            while(!stopPressed)
            {
                await _blockChainManager.ManageAsync(threads);
            }
        
        }
    }
}