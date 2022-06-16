namespace SC4MM
{
    public class ModAndDesiredFiles : IModAndDesiredFiles
    {
        public IMod Mod { get; }
        public IDesiredModFiles DesiredFiles { get; }

        public ModAndDesiredFiles(IMod mod, IDesiredModFiles desiredFiles)
        {
            this.Mod = mod;
            this.DesiredFiles = desiredFiles;
        }

        public void Apply()
        {
            DesiredFiles.Apply();
            Mod.Enable();
        }
    }
}
