using BlockChain.DataAccessors;
using BlockChain.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    public class BlockChain
    {
        private readonly List<Block> _chain;
        private readonly IRetrieveBlockChain _blockChainRetriever;

        public BlockChain()
        {
            _chain = _blockChainRetriever.GetBlockChain().ToList();
        }

        public Block[] Chain
        {
            get
            {
                return _chain.ToArray();
            }
        }

        public bool VerifyChain()
        {
            throw new NotImplementedException();
        }

        public void AddBlock(Block block)
        {
            _chain.Add(block);
        }

        public void RemoveBlock(Block block)
        {
            _chain.Remove(block);
        }

        public string LastBlockHash()
        {
            return _chain.Last().BlockHash;
        }

        public void GenerateNewBlock()
        {
            AddBlock(_blockChainManager.GenerateBlock());
        }
    }
}
