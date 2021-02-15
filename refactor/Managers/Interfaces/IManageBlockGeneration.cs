using BlockChain.Models;

namespace BlockChain.Managers.Interfaces
{
    internal interface IManageBlockGeneration
    {
        Block GenerateNewBlock();
    }
}