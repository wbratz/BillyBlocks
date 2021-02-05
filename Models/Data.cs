using System;

namespace bc.Models
{
    public sealed class Data
    {
        public Transaction[] Transactions { get; set; }
        public string PreviousBlockHash { get; set; }
    }

    public sealed class Transaction 
    {
        public string TransactionID { get; set;}
        public DateTime TransactionDateTime { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}