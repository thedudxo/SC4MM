namespace SC4MM.Tests
{
    class MockMod : IMod
    {
        public bool Enabled { get; private set; }

        readonly Dictionary<string, bool> toggleByFilename;

        public MockMod(Dictionary<string, bool> toggleByFilename)
        {
            this.toggleByFilename = toggleByFilename;
        }

        public MockMod()
        {
            toggleByFilename = new();
        }

        public void Disable()
        {
            Enabled = false;
        }

        public void DisableFile(string filename)
        {
            toggleByFilename[filename] = false;
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void EnableFile(string filename)
        {
            toggleByFilename[filename] = true;
        }
    }
}
