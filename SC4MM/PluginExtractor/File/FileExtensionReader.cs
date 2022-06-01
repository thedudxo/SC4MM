using System.IO;

namespace SC4MM.ZipFileIntake
{
    class FileExtensionReader
    {
        public static SC4FileType GetSC4FileType(string file)
        {
            return StringToEnum(Path.GetExtension(file));
        }

        static SC4FileType StringToEnum(string fileExtension)
        {
            return fileExtension switch
            {
                ".SC4Lot" => SC4FileType.SC4Lot,
                ".SC4Desc" => SC4FileType.SC4Desc,
                ".SC4Model" => SC4FileType.SC4Model,
                ".bat" => SC4FileType.bat,
                ".txt" => SC4FileType.txt,
                ".html" => SC4FileType.html,
                ".jpg" => SC4FileType.jpg,
                ".gif" => SC4FileType.gif,
                _ => SC4FileType.unrecognised
            };
        }
    }
}