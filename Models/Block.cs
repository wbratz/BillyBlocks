using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using bc.PowFunctions;

namespace bc.Models
{
    public class Block
    {
        private readonly string _prevBlockHash;
        private readonly Data _data; 

        private string _blockHash;
        public Block(string prevBlockHash, Data data)
        {
            _data = data;
            _prevBlockHash = prevBlockHash;
            _blockHash = GetBlockHash();   
        }

        public string PrevBlockHash { get {return _prevBlockHash;} }
        public string BlockHash 
        { 
            get 
            { 
                return _blockHash;
            }

            set 
            {
                _blockHash = GetBlockHash();
            } 
        }
        public Data Data { get { return _data; } }

        private string GetBlockHash()
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(Data.ToByteArray());
            
            var miner = new Miner();
            miner.MineBlock(1, ConvertToString(hash));

            return ConvertToString(hash);
        }

        private string ConvertToString(byte[] array)
        {
            var result = "";

            for (int i = 0; i < array.Length; i++)
            {
                result+=$"{array[i]:X2}";
            }

            return result;
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

    internal static class DataExtensions
    {
        internal static byte[] ToByteArray(this Data data)
        {
            var xs = new XmlSerializer(typeof(Data));
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, data);
                return ms.ToArray();
            }
        }
    }
}