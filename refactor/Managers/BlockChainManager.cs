using BlockChain.DataAccessors;
using BlockChain.Managers.Interfaces;
using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Managers
{
    internal class BlockChainManager : IManageBlockChain
    {
        private readonly IManageBlockGeneration _blockGenerator;
        private readonly IPushBlockChain _blockChainPusher;
        private readonly Models.BlockChain _blockChain;

        public BlockChainManager(IManageBlockGeneration blockGenerator,
                                 IPushBlockChain blockChainPusher)
        {
            _blockGenerator = blockGenerator;
            _blockChainPusher = blockChainPusher;
            _blockChain = new Models.BlockChain();
        }

        public Block GenerateBlock() => _blockGenerator.GenerateNewBlock();

        public Models.BlockChain GetBlockChain() => _blockChain;

        public bool PushBlockChain(Block[] blockChain) => _blockChainPusher.Push(blockChain);

        private Models.BlockChain VerifyBlockChain()
        {
            if (_blockChain.VerifyChain())
                return _blockChain;

            throw new Exception();
        }
    }
}
