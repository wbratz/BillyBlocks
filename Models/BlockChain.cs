using System;
using System.Collections;
using System.Collections.Generic;

namespace bc.Models
{
    public class BlockChain : IEnumerable
    {
        private List<Block> _blockChain;

        public BlockChain(List<Block> blockList)
        {
            _blockChain = new List<Block>();

            _blockChain.AddRange(blockList);
        }
        public BlockChain(Block[] blockArray)
        {
            _blockChain = new List<Block>();

            _blockChain.AddRange(blockArray);
        }

        public List<Block> GetBlockChain()
        {
            return _blockChain;
        }

        public string LastBlockHash 
        { 
            get
            {
                return _blockChain[_blockChain.Count - 1].BlockHash;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public BlockChainEnum GetEnumerator()
        {
            return new BlockChainEnum(_blockChain);
        }
    }

    public class BlockChainEnum : IEnumerator 
    {
        public List<Block> _blockChain;

        int position = -1;
        
        public BlockChainEnum(List<Block> blockList)
        {
            _blockChain = blockList;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _blockChain.Count);
        }

        public void Reset()
        {
            position = -1;
        }    

        object IEnumerator.Current { get { return Current; } }

        public Block Current 
        {
            get 
            {
                try
                {
                    return _blockChain[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}