﻿using System;
using System.Collections.Generic;
using bc.Models;
using System.Linq;
using bc.ExtensionMethods;

namespace bc
{
    class Program
    {
        static void Main(string[] args)
        {

            var data = new Data
            {
                TransactionDateTime = DateTime.UtcNow,
                SenderID = "sender0",
                RecipientID = "recipient0",
                TransactionAmount = 1.00020002030M,
                PreviousBlockHash = "0"
            };

            var genesisBlock = new Block("0", data);

            var blockChain = new List<Block>();

            blockChain.Add(genesisBlock);
            
            System.Console.WriteLine($"{blockChain[0].BlockHash}");
        }
    }
}