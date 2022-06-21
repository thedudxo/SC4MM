namespace SC4MM
{
    public interface IModList
    {

        HashSet<IModList> SubLists { get; }
        HashSet<IModAndDesiredFiles> Mods { get; }
        HashSet<IModList> Parents { get;}
        bool Contains(IModAndDesiredFiles mod);
        bool Contains(IModList item);
        void Remove(IModAndDesiredFiles mod);
        void Add(IModAndDesiredFiles mod);
        void Apply();
        void ChildModRemoved(IModAndDesiredFiles mod);
    }
}
