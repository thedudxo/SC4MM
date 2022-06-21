namespace SC4MM_UI.Viewmodels
{
    public class Mod : IMod
    {
        public IMod? Model { get; set; }

        public string Name { get; set; } = "";
        public ModFolders Folders { get; set; } = new("", "", "");
        public Dictionary<string, bool> ToggleByFileName { get; set; } = new();
        public List<string> Readmes { get; set; } = new();

        public Mod(IMod other)
        {
            Name = other.Name;
            Folders = other.Folders;
            ToggleByFileName = other.ToggleByFileName;
            Readmes = other.Readmes;
            Model = other;
        }

        public bool Enabled
        {
            get
            {
                _ = Model ?? throw new InvalidOperationException("No underlying model set"); 
                return Model.Enabled;
            }
        }

        public void Disable()
        {
            _ = Model ?? throw new InvalidOperationException("No underlying model set");
            Model.Disable();
        }

        public void DisableFile(string filename)
        {
            _ = Model ?? throw new InvalidOperationException("No underlying model set");
            Model.DisableFile(filename);
        }

        public void Enable()
        {
            _ = Model ?? throw new InvalidOperationException("No underlying model set");
            Model.Enable();
        }

        public void EnableFile(string filename)
        {
            _ = Model ?? throw new InvalidOperationException("No underlying model set");
            Model.EnableFile(filename);
        }
    }
}
