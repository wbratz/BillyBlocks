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
using System.Threading;

namespace bc.BlockManagers
{
    internal sealed class BlockChainManager : IManageBlockChain
    {
        private readonly IRetrieveBlockChain _blockChainRetriever;
        private readonly ILogger _logger;
        private readonly IMineBlocks _blockMiner;
        private readonly IHash _hasher;
        private readonly IRetrieveData _dataRetriever;
        private List<Block> _blockChain;

        public BlockChainManager(IRetrieveBlockChain blockChainRetriever
            , ILogger<BlockChainManager> logger
            , IMineBlocks blockMiner
            , IHash hasher
            , IRetrieveData dataRetriever)
        {
            _blockChainRetriever = blockChainRetriever;
            _logger = logger;
            _blockMiner = blockMiner;
            _hasher = hasher;
            _dataRetriever = dataRetriever;
            InitializeBlockChain();
        }

        public List<Block> Manage()
        {
            if (_blockChain == null || !_blockChain.Any())
            {
                InitializeBlockChain();
            }

            _blockChain.Add(_blockMiner.MineBlock());

            BlockChain.Chain = _blockChain;

            _logger.LogInformation("New block added");
            _logger.LogInformation("Current BlockChain contents.");

            DisplayBlockChainContents();

            return _blockChain;
        }

        public async Task<List<Block>> ManageAsync(int threads)
        {
            if (_blockChain == null || !_blockChain.Any())
            {
                InitializeBlockChain();
            }

            var block = await _blockMiner.MineBlockAsync(threads);

            if (block != null)
            {
                _blockChain.Add(block);

                BlockChain.Chain = _blockChain;

                _logger.LogInformation("New block added");
                _logger.LogInformation("Current BlockChain contents.");

                DisplayBlockChainContents();
            }

            return _blockChain;
        }

        private void DisplayBlockChainContents()
        {
            for (int i = 0; i < _blockChain.Count; i++)
            {
                _logger.LogInformation($"Block {i} \t Block hash: {_blockChain[i].BlockHash}" +
                    $"\n Previous Block Hash:{_blockChain[i].PrevBlockHash}" +
                    $"\n Block Data: " +
                    $"\n Previous Block Hash: {_blockChain[i].Data.PreviousBlockHash}" +
                    $"\n Number of transactions contained: {_blockChain[i].Data.Transactions.Length}" +
                    $"\n Sample transaction " +
                    $"\n Recipient: {_blockChain[i].Data.Transactions[0].RecipientID}" +
                    $"\n Sender: {_blockChain[i].Data.Transactions[0].SenderID}" +
                    $"\n Transaction Amount: {_blockChain[i].Data.Transactions[0].TransactionAmount}" +
                    $"\n TransactionID: {_blockChain[i].Data.Transactions[0].TransactionID}" +
                    $"\n Transaction DateTime: {_blockChain[i].Data.Transactions[0].TransactionDateTime}");
            }
        }

        private void InitializeBlockChain()
        {
            var blockChain = _blockChainRetriever.GetBlockChain().Result;

            if (_blockChainRetriever != null && !blockChain.Any())
            {
                blockChain.Add(CreateGenesisBlock());
            }

            _blockChain = blockChain;
            BlockChain.Chain = _blockChain;
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
                        TransactionID = _hasher.CalculateHash($"{DateTime.UtcNow.ToLongTimeString()}{0.0M}0000000000000000000000000000000000000000"+"0")
                    }
                }
            };

            return new Block(genesisData, genesisData.Transactions[0].TransactionID);
        }
    }
}