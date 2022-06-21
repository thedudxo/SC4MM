namespace SC4MM
{
    public interface IMod
    {
        ModFolders Folders { get; }
        Dictionary<string, bool> ToggleByFileName { get; }
        List<string> Readmes { get;}
        string Name { get; }

        bool Enabled { get; }
        void Disable();
        void DisableFile(string filename);
        void Enable();
        void EnableFile(string filename);
    }
}