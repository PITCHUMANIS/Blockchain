namespace PropertyOwnershipRegistration.Block
{
    public interface IBlockChain
    {
        void AcceptBlock(IBlock block);
        int NextBlockNumber { get; }
        void VerifyChain();
    }
}
