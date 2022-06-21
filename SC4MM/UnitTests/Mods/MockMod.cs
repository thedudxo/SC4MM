namespace SC4MM.Tests
{
    class MockMod : IMod
    {
        public bool Enabled { get; private set; }

        public ModFolders Folders => throw new NotImplementedException();

        public List<string> Readmes => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public Dictionary<string, bool> ToggleByFileName { get; }

        public MockMod(Dictionary<string, bool> toggleByFilename)
        {
            this.ToggleByFileName = toggleByFilename;
        }

        public MockMod()
        {
            ToggleByFileName = new();
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void DisableFile(string filename)
        {
            ToggleByFileName[filename] = false;
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void EnableFile(string filename)
        {
            ToggleByFileName[filename] = true;
        }
    }
}
