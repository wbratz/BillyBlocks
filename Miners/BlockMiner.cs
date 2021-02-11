using System.Globalization;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using bc.Models;
using bc.HashingMethods;
using Microsoft.Extensions.Logging;
using bc.BlockManagers;
using System.Threading;

namespace bc.Miners
{
    public class BlockMiner : IMineBlocks
    {
        private long nonce = 0;
        private readonly int _difficulty;
        private readonly IHash _hasher;
        private readonly ILogger _logger;
        private readonly IGenerateNewBlocks _newBlockManager;
        private readonly string _comparison;
        private string hash;
        private bool isFound;

        public BlockMiner(
            IHash hasher,
            IGenerateNewBlocks newBlockManager,
            ILogger<BlockMiner> logger)
        {
            _difficulty = 5;
            _comparison = new string('0', _difficulty + 1);
            _hasher = hasher;
            _newBlockManager = newBlockManager;
            _logger = logger;
            isFound = false;

            hash = Guid.NewGuid().ToString();
        }

        public Block MineBlock()
        {
            if (hash.Substring(0, _difficulty + 1) == _comparison)
            {
                GenerateNewHash();
            }

            var sw = new Stopwatch();

            _logger.LogInformation($"Miner started.");

            sw.Start();

            while (hash.Substring(0, _difficulty + 1) != _comparison)
            {
                nonce++;
                hash = _hasher.CalculateHash(hash + nonce.ToString());
            }

            sw.Stop();

            _logger.LogInformation($"Mining complete. Total time: {sw.Elapsed}");

            return _newBlockManager.GenerateNewBlockAsync(hash).Result;
        }

        public async Task<Block> MineBlockAsync(int threads)
        {
            if (hash.Substring(0, _difficulty + 1) == _comparison)
            {
                GenerateNewHash();
            }

            threads = threads > 0 ? threads : 1;

            var taskList = new List<Task<string>>();

            var nonceRangeDictionary = GetNonceRangeForThreads(threads);

            _logger.LogInformation($"Starting async mining on {threads} threads.");

            foreach (var nonceRange in nonceRangeDictionary)
            {
            }

            var workTask = await Task.WhenAny(taskList);

            var completedWork = await workTask;

            _logger.LogInformation($"Async mining complete.");

            if (completedWork != null)
            {
                return await _newBlockManager.GenerateNewBlockAsync(completedWork);
            }

            return null;
        }

        private string DoWork(long nonceStart, long nonceEnd)
        {
            var asyncNonce = nonceStart;
            var asyncHash = Guid.NewGuid().ToString();
            isFound = false;

            while (asyncHash.Substring(0, _difficulty + 1) != _comparison)
            {
                if (isFound)
                {
                    _logger.LogInformation("Process halted, another thread found solution");
                    return null;
                }
                if (asyncNonce > nonceEnd || asyncNonce < 0)
                {
                    return null;
                }

                asyncNonce++;
                asyncHash = _hasher.CalculateHash(hash + asyncNonce.ToString());
            }

            _logger.LogInformation("Solution Found");
            isFound = true;

            return asyncHash;
        }

        private Dictionary<int, (long startValue, long endValue)> GetNonceRangeForThreads(int threads)
        {
            var result = new Dictionary<int, (long, long)>();
            long placeHolder = 0;

            var mod = (long.MaxValue % threads);
            var quotant = (long.MaxValue - mod) / threads;

            for (int i = 0; i < threads; i++)
            {
                if (i == threads - 1)
                {
                    result.Add(i, (placeHolder + 1, placeHolder + quotant + mod));
                }
                else
                {
                    result.Add(i, (placeHolder + 1, placeHolder + quotant));
                }

                placeHolder += quotant;
            }

            return result;
        }

        private void GenerateNewHash()
        {
            _logger.LogInformation($"Generating new hash, current hash: {hash}");
            while (hash.Substring(0, _difficulty + 1) == _comparison)
            {
                hash = Guid.NewGuid().ToString();
            }
        }
    }
}