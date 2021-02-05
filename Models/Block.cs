using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using bc.PowFunctions;
using System.Threading.Tasks;

namespace bc.Models
{
    public class Block
    {
        private readonly string _prevBlockHash;
        private readonly Data _data;
        private string _hashBlock;

        public Block(string prevBlockHash, Data data)
        {
            _data = data;
            _prevBlockHash = prevBlockHash;
        }

        public string PrevBlockHash { get { return _prevBlockHash; } }

        public string BlockHash { get { return _hashBlock; } }

        public Data Data { get { return _data; } }

        public async Task MineHashBlock()
        {
            var miner = new Miner();

            _hashBlock = await miner.MineBlock(8, _data);
        }
    }

    public class Data
    {
        public DateTime TransactionDateTime { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string PreviousBlockHash { get; set; }
    }
}