namespace SC4MM
{
    public interface IModList
    {
        public HashSet<IModAndDesiredFiles> Mods { get; }
        bool Contains(IModAndDesiredFiles mod);

        void Apply();
    }
}
