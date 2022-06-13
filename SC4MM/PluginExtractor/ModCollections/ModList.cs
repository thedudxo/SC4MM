namespace SC4MM
{
    public class ModList : IModList
    {
        readonly HashSet<IModList> SubLists = new();
        readonly HashSet<IModAndDesiredFiles> ChildMods = new();
        public HashSet<IModAndDesiredFiles> Mods { get;} = new();
        public HashSet<IModList> Parents { get; } = new();

        public void AddSublist(IModList sublist)
        {
            if (Contains(sublist))
                throw new ArgumentException("Sublist was allready in the collection", nameof(sublist));

            if (Parents.Contains(sublist))
                throw new ArgumentException("The list you are trying to add, is a parent of this list");

            sublist.Parents.Add(this);
            SubLists.Add(sublist);
            ChildMods.UnionWith(sublist.Mods);
        }

        public void Add(IModAndDesiredFiles mod)
        {
            if(Mods.Contains(mod))
                throw new ArgumentException("Mod was allready in the collection", nameof(mod));

            if (ChildMods.Contains(mod))
                throw new ArgumentException("Mod was allready contained in a child list of this list.");

            Mods.Add(mod);
        }

        public void RemoveSublist(IModList sublist)
        {
            if(Contains(sublist) == false)
                throw new ArgumentException("Sublist was not in the collection", nameof(sublist));

            SubLists.Remove(sublist);
            ChildMods.ExceptWith(sublist.Mods);
        }

        public void Remove(IModAndDesiredFiles mod)
        {
            if (Mods.Contains(mod) == false)
                throw new ArgumentException("Mod was not in the collection", nameof(mod));

            Mods.Remove(mod);
            foreach (IModList parent in Parents)
                parent.ChildModRemoved(mod);
        }

        public void ChildModRemoved(IModAndDesiredFiles mod)
        {
            ChildMods.Remove(mod);
        }

        public void Apply()
        {
            foreach (var mod in Mods)
                mod.Apply();

            foreach (var subList in SubLists)
                subList.Apply();
        }

        public bool Contains(IModList item)
        {
            return SubLists.Contains(item);
        }

        public bool Contains(IModAndDesiredFiles mod)
        {
            return Mods.Contains(mod) || ChildMods.Contains(mod);
        }
    }
}
