namespace SC4MM
{
    public class ModList : IModList
    {
        readonly HashSet<IModList> SubLists = new();
        public HashSet<IModAndDesiredFiles> Mods { get; private set; } = new();

        public void AddSublist(IModList sublist)
        {
            if (Contains(sublist))
                throw new ArgumentException("Sublist was allready in the collection", nameof(sublist));

            SubLists.Add(sublist);
            Mods.UnionWith(sublist.Mods);
        }

        public void Add(IModAndDesiredFiles mod)
        {
            if(Mods.Contains(mod))
                throw new ArgumentException("Mod was allready in the collection", nameof(mod));

            Mods.Add(mod);
        }

        public void RemoveSublist(IModList item)
        {
            if(Contains(item) == false)
                throw new ArgumentException("Sublist was not in the collection", nameof(item));

            SubLists.Remove(item);
        }

        public void Remove(IModAndDesiredFiles mod)
        {
            if (Mods.Contains(mod) == false)
                throw new ArgumentException("Mod was not in the collection", nameof(mod));

            Mods.Remove(mod);
        }

        public void Apply()
        {
            foreach (IModList sublist in SubLists)
                sublist.Apply();

            foreach (var mod in Mods)
                mod.Apply();
        }

        public bool Contains(IModList item)
        {
            return SubLists.Contains(item);
        }

        public bool Contains(IModAndDesiredFiles mod)
        {
            return Mods.Contains(mod);        
        }
    }
}
