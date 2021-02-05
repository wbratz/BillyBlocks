using System.Threading.Tasks;
using bc.Models;
using bc.Retievers;

namespace bc.BlockManagers
{
    public class NewBlockManager : IManageNewBlocks
    {
        private IRetrieveData _dataRetriever;
        public NewBlockManager(IRetrieveData dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public async Task<Block> GenerateNewBlock(string hash)
        {
            var data = await _dataRetriever.GetData();

            return new Block(data, hash);
        }
    }
}