using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DataAccessors
{
    internal interface IPushBlockChain
    {
        bool Push(Block[] blockChain);
    }
}
