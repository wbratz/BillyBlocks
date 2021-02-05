using System.Collections.Generic;
using System.Threading.Tasks;
using bc.Models;

namespace bc.Retievers
{
    public class BlockChainRetriever : IRetrieveBlockChain
    {
        public Task<List<Block>> GetBlockChain()
        {
            throw new System.NotImplementedException();
        }
    }
}