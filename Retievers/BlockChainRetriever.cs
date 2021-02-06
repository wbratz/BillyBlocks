using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bc.HashingMethods;
using bc.Models;

namespace bc.Retievers
{
    public class BlockChainRetriever : IRetrieveBlockChain
    {
        private readonly IHash _hasher;
        private List<Block> _blockChain;

        public BlockChainRetriever(IHash hasher)
        {
            _hasher = hasher;
            _blockChain = InitializeBlockChain();
        }

        public async Task<List<Block>> GetBlockChain()
        {
            return _blockChain;
        }

        private List<Block> InitializeBlockChain()
        {
            var genesisData = new Data
            {
                PreviousBlockHash = "0",
                Transactions = new Transaction[]
                {
                    new Transaction
                    {
                        TransactionDateTime = DateTime.UtcNow,
                        TransactionAmount = 0.0M,
                        SenderID = "00000000000000000000",
                        RecipientID = "00000000000000000000",
                        TransactionID = _hasher.CalculateHash($"{DateTime.UtcNow.ToLongTimeString()}{0.0M}0000000000000000000000000000000000000000"+"0")
                    }
                }
            };

            var block = new Block(genesisData, genesisData.Transactions[0].TransactionID);

            return new List<Block> { block };
        }
    }
}