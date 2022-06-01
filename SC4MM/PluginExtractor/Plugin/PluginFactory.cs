using System;
using System.IO;
using System.Collections.Generic;

namespace SC4MM.ZipFileIntake
{
    public class PluginFactory
    {
        readonly IFilePathReader pathReader;

        public PluginFactory(IFilePathReader pathReader)
        {
            this.pathReader = pathReader;
        }

        public Plugin Open(string folderPath)
        {
            string[] paths = Directory.GetFiles(folderPath);

            var pluginFiles = new List<PluginFile>();
            var ReadmeFiles = new List<ReadmeFile>();

            foreach (string filePath in paths)
            {
                //string name = pathReader.ReadName(filePath);
                string extension = pathReader.ReadExtension(filePath);

                switch (pathReader.GetFileType(extension))
                {
                    case (FileType.SC4Plugin):
                        pluginFiles.Add(new PluginFile());
                        break;

                    case (FileType.Readme):
                        ReadmeFiles.Add(new ReadmeFile());
                        break;

                    case (FileType.Unrecognised):
                        break;
                }
            }

            Plugin plugin = new(pluginFiles, ReadmeFiles);
            return plugin;
        }
    }
}