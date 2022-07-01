namespace SC4MM_UI.Viewmodels
{
    internal class ModList
    {
        public IModList? Model { get; set; }
        public List<ModAndDesiredFiles> Mods { get; set; } = new();
        public List<ModList> SubLists { get; set; } = new();

        public string Name { get; set; } = "";
        
        public ModList()
        {

        }

        public ModList(IModList model)
        {
            foreach (IModAndDesiredFiles mod in model.Mods)
            {
                Mods.Add(new ModAndDesiredFiles(mod));
            }

            foreach (IModList list in model.SubLists)
            {
                SubLists.Add(new ModList(list));
            }

            Model = model;

            Name = model.Name;
        }
    }
}
