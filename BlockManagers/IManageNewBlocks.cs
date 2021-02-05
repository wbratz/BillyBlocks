using System.Threading.Tasks;
using bc.Models;

namespace bc.BlockManagers
{
    public interface IManageNewBlocks
    {
        Task<Block> GenerateNewBlock(string hash);
    }
}