using BlockChain.Managers.Interfaces;
using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Managers
{
    internal class BlockGenerationManager : IManageBlockGeneration
    {
        private readonly IManageTransactions _transactionManager;
        private readonly IManageBlockMining _blockMiningManager;

        internal BlockGenerationManager(IManageTransactions transactionManager,
                                      IManageBlockMining blockMiningManager)
        {
            _transactionManager = transactionManager;
            _blockMiningManager = blockMiningManager;
        }

        public Block GenerateNewBlock()
        {
            _blockMiningManager.MineBlock(Transaction[] transactions);
        }
    }
}
