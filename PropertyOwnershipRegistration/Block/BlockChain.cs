﻿using System;
using System.Collections.Generic;

namespace PropertyOwnershipRegistration.Block
{
    public class BlockChain : IBlockChain
    {
        public IBlock CurrentBlock { get; private set; }
        public IBlock HeadBlock { get; private set; }

        public List<IBlock> Blocks { get; }

        public BlockChain()
        {
            Blocks = new List<IBlock>();
        }

        public void AcceptBlock(IBlock block)
        {
            // This is the first block, so make it the genesis block.
            if (HeadBlock == null)
            {
                HeadBlock = block;
                HeadBlock.PreviousBlockHash = null;
            }

            CurrentBlock = block;
            Blocks.Add(block);
        }

        public int NextBlockNumber
        {
            get
            {
                if (HeadBlock == null)
                {
                    return 0;
                }

                return CurrentBlock.BlockNumber + 1;
            }
        }

        public void VerifyChain()
        {
            if (HeadBlock == null)
            {
                throw new InvalidOperationException("Genesis block not set.");
            }

            var isValid = HeadBlock.IsValidChain(null, true);

            Console.WriteLine(isValid ? "Blockchain integrity intact." : "Blockchain integrity NOT intact.");
        }
    }
}
