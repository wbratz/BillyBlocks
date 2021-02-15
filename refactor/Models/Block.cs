using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    public class Block
    {
        private readonly string _blockHash;
        private readonly string _previousBlockHash;
        private readonly Transaction[] _transactions;
        private readonly string _merkleRoot;

        public Block(string blockHash, 
                     string previousBlockHash, 
                     Transaction[] transactions, 
                     string merkleRoot)
        {
            _blockHash = blockHash;
            _previousBlockHash = previousBlockHash;
            _transactions = transactions;
            _merkleRoot = merkleRoot;
        }

        public string BlockHash 
        {
            get
            {
                return _blockHash;
            }
        }
        public string PreviousBlockHash 
        {
            get 
            {
                return _previousBlockHash;
            } 
        }
        public Transaction[] Transactions 
        {
            get
            {
                return _transactions;
            }
        }
        public string MerkleRoot 
        {
            get 
            {
                return _merkleRoot;
            } 
        }
    }
}
