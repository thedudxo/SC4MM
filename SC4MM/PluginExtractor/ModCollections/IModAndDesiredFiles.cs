namespace SC4MM
{
    public interface IModAndDesiredFiles
    {
        public IMod Mod { get; }
        public IDesiredModFiles DesiredFiles { get; }

        void Apply();
    }
}