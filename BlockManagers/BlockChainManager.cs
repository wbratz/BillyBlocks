using System.Transactions;
using System.Collections.Generic;
using bc.Models;
using bc.Miners;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using System;
using bc.HashingMethods;
using bc.Retievers;

namespace bc.BlockManagers
{
    internal sealed class BlockChainManager : IManageBlockChain
    {
        private readonly IRetrieveBlockChain _blockChainRetriever;
        private readonly ILogger _logger;
        private readonly IMineBlocks _blockMiner;
        private readonly IHash _hasher;

        public BlockChainManager(IRetrieveBlockChain blockChainRetriever
            , ILogger<BlockChainManager> logger
            , IMineBlocks blockMiner
            , IHash hasher)
        {
                _blockChainRetriever = blockChainRetriever;
                _logger = logger;
                _blockMiner = blockMiner;
                _hasher = hasher;
        }

        public List<Block> Manage()
        {
            var blockChain = GetBlockChain();

            blockChain.Add(_blockMiner.MineBlock());

            return blockChain;
        }

        public async Task<List<Block>> ManageAsync(int threads)
        {
            var blockChain = GetBlockChain();

            var block = await _blockMiner.MineBlockAsync(threads);

            blockChain.Add(block);

            return blockChain;
        }      

        private List<Block> GetBlockChain()
        {
            var blockChain = _blockChainRetriever.GetBlockChain().Result;

            if(_blockChainRetriever != null && !blockChain.Any())
            {
                 blockChain.Add(CreateGenesisBlock());
                 return blockChain;
            }

            return blockChain;
        }

        private Block CreateGenesisBlock()
        {
            var genesisData = new Data
            {
                PreviousBlockHash = "0",
                Transactions = new Models.Transaction[]
                {
                    new Models.Transaction 
                    {
                        TransactionDateTime = DateTime.UtcNow,
                        TransactionAmount = 0.0M,
                        SenderID = "00000000000000000000",
                        RecipientID = "00000000000000000000",
                        TransactionID = _hasher.CalculateHash($"{DateTime.UtcNow.ToLongTimeString()}{0.0M.ToString()}0000000000000000000000000000000000000000")
                    }
                }
            };

            return new Block(genesisData, genesisData.Transactions[0].TransactionID);
        }    
    }
}