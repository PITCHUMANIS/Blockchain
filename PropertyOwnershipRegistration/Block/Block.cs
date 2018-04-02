using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Clifton.Blockchain;
using PropertyOwnershipRegistration.Cryptography;
using PropertyOwnershipRegistration.Model;

namespace PropertyOwnershipRegistration.Block
{
    public class Block : IBlock
    {
        public List<ITransaction> Transaction { get; }
        
        public int BlockNumber { get; }
        public DateTime CreatedDate { get; set; }
        public string BlockHash { get; private set; }
        public string PreviousBlockHash { get; set; }
        public string BlockSignature { get; private set; }
        public int MiningDifficulty { get; }
        public int Nonce { get; private set; }

        public IBlock NextBlock { get; set; }

        private MerkleTree merkleTree = new MerkleTree();
        public IKeyStore KeyStore { get; }

        public Block(int blockNumber, int miningDifficulty)
        {
            BlockNumber = blockNumber;

            CreatedDate = DateTime.UtcNow;
            Transaction = new List<ITransaction>();
            MiningDifficulty = miningDifficulty;
        }

        public Block(int blockNumber, IKeyStore keystore, int miningDifficulty)
        {
            BlockNumber = blockNumber;

            CreatedDate = DateTime.UtcNow;
            Transaction = new List<ITransaction>();
            KeyStore = keystore;
            MiningDifficulty = miningDifficulty;
        }

        public void AddTransaction(ITransaction transaction)
        {
            Transaction.Add(transaction);
        }

        public string CalculateBlockHash(string previousBlockHash)
        {
            var blockheader = BlockNumber + CreatedDate.ToString(CultureInfo.InvariantCulture) + previousBlockHash;
            var combined = merkleTree.RootNode + blockheader;
            
            var completeBlockHash = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(combined)));
            
            return completeBlockHash;
        }
        
        public void SetBlockHash(IBlock parent)
        {
            if (parent != null)
            {
                PreviousBlockHash = parent.BlockHash;
                parent.NextBlock = this;
            }
            else
            {
                // Previous block is the genesis block.
                PreviousBlockHash = null;
            }

            BuildMerkleTree();
            
            BlockHash = CalculateProofOfWork(CalculateBlockHash(PreviousBlockHash));

            if (KeyStore != null)
            {
                BlockSignature = KeyStore.SignBlock(BlockHash);
            }
        }

        private void BuildMerkleTree()
        {
            merkleTree = new MerkleTree();

            foreach (var txn in Transaction)
            {
                merkleTree.AppendLeaf(MerkleHash.Create(txn.CalculateTransactionHash()));
            }

            merkleTree.BuildTree();
        }

        public string CalculateProofOfWork(string blockHash)
        {
            var difficulty = DifficultyString();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                var hashedData = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + blockHash)));

                if (hashedData.StartsWith(difficulty, StringComparison.Ordinal))
                {
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value.
                    var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds/10:00}";
                    
                    Console.WriteLine("Time elapsed to mine an appropriate hash for Block-" + BlockNumber + " = " + elapsedTime + " - " + hashedData);
                    return hashedData;
                }

                Nonce++;
            }
        }

        private string DifficultyString()
        {
            var difficultyString = string.Empty;

            for (var i = 0; i < MiningDifficulty; i++)
            {
                difficultyString += "0";
            }

            return difficultyString;
        }

        public bool IsValidChain(string prevBlockHash, bool verbose)
        {
            var isValid = true;

            BuildMerkleTree();
            
            var newBlockHash = Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(Nonce + CalculateBlockHash(prevBlockHash))));

            var validSignature = KeyStore.VerifyBlock(newBlockHash, BlockSignature);

            if (newBlockHash != BlockHash)
            {
                isValid = false;
            }
            else
            {
                // check whether previous block hash match the latest previous block hash
                isValid |= PreviousBlockHash == prevBlockHash;
            }

            PrintVerificationMessage(verbose, isValid, validSignature);

            // Check the next block by passing in our newly calculated blockhash. This will be compared to the previous
            // hash in the next block. They should match for the chain to be valid.
            if (NextBlock != null)
            {
                return NextBlock.IsValidChain(newBlockHash, verbose);
            }

            return isValid;
        }

        private void PrintVerificationMessage(bool verbose, bool isValid, bool validSignature)
        {
            if (verbose)
            {
                if (!isValid)
                {
                    Console.WriteLine("Block Number " + BlockNumber + " : Failed Verification");
                }
                else
                {
                    Console.WriteLine("Block Number " + BlockNumber + " : Pass Verification");
                }

                if (!validSignature)
                {
                    Console.WriteLine("Block Number " + BlockNumber + " : Invalid Digital Signature");
                }
            }
        }
    }
}
