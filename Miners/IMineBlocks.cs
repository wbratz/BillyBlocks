using System.Threading.Tasks;
using bc.Models;

namespace bc.Miners
{
    public interface IMineBlocks
    {
        Task<Block> MineBlockAsync(int threads);
        Block MineBlock();
    }
}