using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DataAccessors
{
    public class BlockChainRetriever : IRetrieveBlockChain
    {
        private readonly Block[] _blockArray;

        public BlockChainRetriever()
        {
            _blockArray = RetrieveBlockChainAsync().Result;
        }

        public Block[] GetBlockChain()
        {
                return _blockArray;
        }

        private async Task<Block[]> RetrieveBlockChainAsync()
        {
            throw new NotImplementedException();
        }
    }
}
