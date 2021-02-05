namespace bc.Models
{
    public class Block
    {
        private readonly string _prevBlockHash;
        private string _blockHash;
        private Data _data;

        public Block(Data data, string blockHash)
        {
            _data = data;
            _blockHash = blockHash;
            _prevBlockHash = data.PreviousBlockHash;
        }

        public string PrevBlockHash { get { return _prevBlockHash; } private set { PrevBlockHash = _prevBlockHash ;} }

        public string BlockHash { get { return _blockHash; } private set { BlockHash = _blockHash; } }

        public Data Data 
        { 
            get 
            {
                return _data; 
            } 
            private set 
            { 
                Data = _data; 
            }
        }
    }
}