using bc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bc.BlockManagers
{
    public interface IGenerateNewBlocks
    {
        Task<Block> GenerateNewBlockAsync(string hash);
    }
}