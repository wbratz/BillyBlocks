using System.Collections.Generic;
using System.Threading.Tasks;
using bc.Models;

namespace bc.BlockManagers
{
    public interface IManageBlockChain
    {
        Task<List<Block>> ManageAsync(int threads);
        List<Block> Manage();
    }
}