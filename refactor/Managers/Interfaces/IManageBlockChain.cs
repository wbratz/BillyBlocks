using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Managers.Interfaces
{
    internal interface IManageBlockChain
    {
        Models.BlockChain GetBlockChain();
        bool PushBlockChain(Block[] blockChain);
        Block GenerateBlock();
    }
}
