namespace SC4MM
{
    public class DesiredModFiles : IDesiredModFiles
    {
        readonly IMod mod;
        public List<string> IncludedFiles { get; } = new();
        public List<string> ExcludedFiles { get; } = new();

        public DesiredModFiles(IMod mod)
        {
            this.mod = mod;
        }

        public void Apply()
        {
            foreach (string file in IncludedFiles)
                mod.EnableFile(file);

            foreach (string file in ExcludedFiles)
                mod.DisableFile(file);
        }
    }
}