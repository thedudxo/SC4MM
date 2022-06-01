using System.Collections.Generic;

namespace SC4MM.ZipFileIntake
{
    public class Plugin
    {
        public List<PluginFile> PluginFiles { get; private set; }
        public List<ReadmeFile> ReadmeFiles { get; private set; }

        public Plugin(List<PluginFile> Files, List<ReadmeFile> Readmes)
        {
            this.PluginFiles = Files;
            this.ReadmeFiles = Readmes;
        }
    }
}