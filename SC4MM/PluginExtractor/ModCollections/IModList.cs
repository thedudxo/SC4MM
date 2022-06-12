namespace SC4MM
{
    public interface IModList
    {
        public HashSet<IModAndDesiredFiles> Mods { get; }
        bool Contains(IModAndDesiredFiles mod);
        public bool Contains(IModList item);

        void Apply();
    }
}
