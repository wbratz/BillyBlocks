using bc.Models;
using bc.Retievers;
using System.Threading.Tasks;

namespace bc.BlockManagers
{
    public class NewBlockGenerator : IGenerateNewBlocks
    {
        private readonly IRetrieveData _dataRetriever;

        public NewBlockGenerator(IRetrieveData dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public async Task<Block> GenerateNewBlockAsync(string hash)
        {
            var data = await _dataRetriever.GetData();

            return new Block(data, hash);
        }
    }
}