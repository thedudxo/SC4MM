namespace SC4MM
{
    public class DesiredModFiles
    {
        readonly IMod mod;
        readonly List<string> includedFiles;
        readonly List<string> excludedFiles;

        public DesiredModFiles(IMod mod, List<string> includedFiles, List<string> excludedFiles)
        {
            this.mod = mod;
            this.includedFiles = includedFiles;
            this.excludedFiles = excludedFiles;
        }

        public void Apply()
        {
           foreach (string file in includedFiles)
                mod.EnableFile(file);

            foreach (string file in excludedFiles)
                mod.DisableFile(file);
        }
    }
}
