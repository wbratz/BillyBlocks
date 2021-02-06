using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bc.BlockManagers;
using bc.HashingMethods;
using bc.Models;

namespace bc.Retievers
{
    public class DataRetriever : IRetrieveData
    {
        private readonly IHash _hasher;

        public DataRetriever(IHash hasher)
        {
            _hasher = hasher;
        }

        public async Task<Data> GetData()
        {
            return await GenerateDataAsync();
        }

        private async Task<Data> GenerateDataAsync()
        {
            return new Data
            {
                PreviousBlockHash = BlockChain.Chain[BlockChain.Chain.Count - 1].BlockHash,
                Transactions = GenerateTransactions(BlockChain.Chain[BlockChain.Chain.Count - 1].BlockHash)
            };
        }

        private Transaction[] GenerateTransactions(string prevHash)
        {
            var rand = new Random();

            var num = rand.Next(2, 30);
            var transactionArray = new Transaction[num];

            for (int i = 0; i < num; i++)
            {
                var sender = Guid.NewGuid().ToString();
                var receiver = Guid.NewGuid().ToString();
                var multiplier = rand.NextDouble();

                transactionArray[i] = new Transaction
                {
                    RecipientID = receiver,
                    SenderID = sender,
                    TransactionDateTime = DateTime.UtcNow.AddDays(-num),
                    TransactionAmount = num * (decimal)multiplier,
                    TransactionID = _hasher.CalculateHash($"{DateTime.UtcNow.AddDays(-num)}{sender}{receiver}{num * (decimal)multiplier}{prevHash}")
                };
            }

            return transactionArray;
        }
    }
}