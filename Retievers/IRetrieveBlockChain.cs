using System.Collections.Generic;
using System.Threading.Tasks;
using bc.Models;

namespace bc.Retievers
{
    public interface IRetrieveBlockChain
    {
         Task<List<Block>> GetBlockChain();
    }
}