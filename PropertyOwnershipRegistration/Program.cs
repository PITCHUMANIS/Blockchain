using System;
using PropertyOwnershipRegistration.Block;
using PropertyOwnershipRegistration.Cryptography;
using PropertyOwnershipRegistration.Model;
using PropertyOwnershipRegistration.Repositories;

namespace PropertyOwnershipRegistration
{
    internal class Program
    {
        static readonly TransactionPool transactionPool = new TransactionPool();
        private static void Main()
        {
            // Get transactions from repository
            var transaction10 = GetTransactions.GetTransactionRecords(transactionPool);

            var chain = new BlockChain();
            // Add transactions to block
            AddTransactionsToBlocks(transactionPool, chain);
            // verify blocks
            chain.VerifyChain();

            // update a transaction in a block
            Console.WriteLine(@"Now let us alter the purchase amount of one of the transaction.");
            transaction10.PurchaseAmount = 620000.00m;
            Console.WriteLine("Purchanse amount of transaction-10 in block-4 is updated from 520000.00m to 620000.00m.");
            // verify blocks
            chain.VerifyChain();
            Console.WriteLine("Verification failed for all the blocks which are beneath block-3");
            Console.ReadKey();
        }

        private static void AddTransactionsToBlocks(TransactionPool tranPool, BlockChain chain)
        {
            var numberOfTransactionsPerBlock = Convert.ToInt32(Properties.Settings.Default["NumberOfTransactionsPerBlock"]);
            var miningDifficultyLevel = Convert.ToInt32(Properties.Settings.Default["MiningDifficultyLevel"]);
            var keyStore = new KeyStore(Hmac.GenerateKey());
            var blockIndex = 0;
            var blockNumber = 1;
            while (tranPool._queue.Count > 0)
            {
                var block = new Block.Block(blockNumber, keyStore, miningDifficultyLevel);
                for (var i = 0; i < numberOfTransactionsPerBlock; i++)
                {
                    if (tranPool._queue.Count > 0)
                    {
                        block.AddTransaction(tranPool.GetTransaction());
                    }
                    else
                    {
                        break;
                    }
                }
                block.SetBlockHash(chain.Blocks.Count <= 0 ? null : chain.Blocks[blockIndex-1]);
                chain.AcceptBlock(block);
                blockIndex++;
                blockNumber++;
            }
        }

    }
}
