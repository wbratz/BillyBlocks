using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using bc.Models;
using bc.HashingMethods;
using Microsoft.Extensions.Logging;
using bc.BlockManagers;

namespace bc.Miners
{
    public class BlockMiner : IMineBlocks
    {
        private long nonce = 0;
        private bool isFound;
        private readonly int _difficulty;
        private readonly Data _data;
        private readonly IHash _hasher;
        private readonly ILogger _logger;
        private readonly IManageNewBlocks _newBlockManager;
        private readonly string _comparison;
        private string hash;

        public BlockMiner(
            IHash hasher,
            IManageNewBlocks newBlockManager, 
            ILogger<BlockMiner> logger)
        {
            _difficulty = 6;
            _comparison = new string('0', _difficulty + 1);
            _hasher = hasher;
            _newBlockManager = newBlockManager;
            _logger = logger;

            hash = Guid.NewGuid().ToString();
        }

        public Block MineBlock()
        {
            if(hash.Substring(0, _difficulty + 1) == _comparison)
            {
                GenerateNewHash();
            }
            
            var sw = new Stopwatch();

            _logger.LogInformation($"Miner started.");

            sw.Start();

            while (hash.Substring(0, _difficulty + 1) != _comparison && isFound)
            {
                nonce++;
                hash = _hasher.CalculateHash(hash + nonce.ToString());
            }

            sw.Stop();
            
            _logger.LogInformation($"Mining complete. Total time: {sw.Elapsed}");

            isFound = true;

            return _newBlockManager.GenerateNewBlock(hash).Result;
        }

        private Block GenreateBlock()
        {
            throw new NotImplementedException();
        }

        public async Task<Block> MineBlockAsync(int threads)
        {            
            threads = threads > 0 ? threads : 1;

            _logger.LogInformation($"Starting async mining on {threads.ToString()} threads.");

            var taskList = new List<Task<Block>>();

            for (int i = 0; i < threads; i++)
            {
                taskList.Add(Task.Run(() => MineBlock()));
            }

            var newBlockTask = await Task.WhenAny(taskList);

            _logger.LogInformation($"Async mining complete.");

            return await newBlockTask;
        }

        private void GenerateNewHash()
        {
            _logger.LogInformation($"Generating new hash, current hash: {hash}");
            while(hash.Substring(0, _difficulty + 1) == _comparison)
            {
                hash = Guid.NewGuid().ToString();
            }
        }

    }
}