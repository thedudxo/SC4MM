namespace SC4MM_UI.Viewmodels
{
    public class Mod
    {
        public readonly SC4MM.Mod mod;

        public string Name { get; set; }
        public ModFolders Folders { get; set; }
        public Dictionary<string, bool> ToggleByFileName { get; set; }
        public List<string> ReadmeFiles { get; set; }

        public Mod(SC4MM.Mod mod)
        {
            this.mod = mod;
            Name = mod.Name;
            Folders = mod.folders;
            ToggleByFileName = mod.ToggleByFileName;
            ReadmeFiles = mod.Readme;
        }
    }
}
