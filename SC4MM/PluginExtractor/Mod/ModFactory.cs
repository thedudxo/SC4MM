using System;
using System.IO;
using System.Collections.Generic;

namespace SC4MM.ZipFileIntake
{
    public class ModFactory
    {
        readonly IFilePathReader pathReader;

        public ModFactory(IFilePathReader pathReader)
        {
            this.pathReader = pathReader;
        }

        //public Mod Open(string folderPath)
        //{
        //    string[] paths = Directory.GetFiles(folderPath);

        //    ModPaths modPaths = new("none","none","none");
        //    ModFiles files = new();

        //    foreach (string filePath in paths)
        //    {
        //        //string name = pathReader.ReadName(filePath);
        //        string extension = pathReader.ReadExtension(filePath);

        //        switch (pathReader.GetFileType(extension))
        //        {
        //            case (FileType.SC4Plugin):
        //                files.Enabled.AddLast(filePath);
        //                break;

        //            case (FileType.Readme):
        //                files.Readme.Add(filePath);
        //                break;

        //            case (FileType.Unrecognised):
        //                break;
        //        }
        //    }

        //    Mod plugin = new(paths, files,);
        //    return plugin;
        //}
    }
}